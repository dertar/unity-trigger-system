using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Trigger
{
    public class EventOnUse : Event
    {
        [Header("Разрешенные слои")]
        [Tooltip("Разрешенные слои для активации триггера")]
        [SerializeField]
        public AllowedLayers layers;

        private List<EventOnUseTransmitter> transmitters = new List<EventOnUseTransmitter>();


        override protected void Init()
        {
            enabled = false;
            countTriggered = 0;
        }

        public void AddTransmitter(EventOnUseTransmitter transmitter)
        {

            transmitters.Add(transmitter);
            Log("Registered new transmitter " + transmitter.ToString());
        }


        public void Signal(GameObject whoTriggered)
        {
            //Log("Signal from " + whoTriggered.ToString());
            if (!IsState(ETriggerStates.FINISHED))
            {
                if (IsAllTransmitter())
                {
                    Run(whoTriggered);
                }
            }
            else
            {
                var subject = whoTriggered.GetComponent<SimpleController>();
                subject.Used();
                //whoTriggered.BroadcastMessage("Used");
                Log("It's used");
            }
            enabled = true;
        }


        override public void End()
        {
            Log("Used");
            whoTriggered.BroadcastMessage("Used");
            SetState(ETriggerStates.FINISHED);

        }

        public override void Update()
        {
            if (IsState(ETriggerStates.RUNNING) || IsState(ETriggerStates.PAUSE))
            {
                if (IsState(ETriggerWorkMode.FIRE))
                {
                    if (queue.Current() == null ||
                        queue.Current().IsState(ETriggerStates.FINISHED))
                    {
                        PopAction(whoTriggered);
                    }

                }
                else if (IsState(ETriggerWorkMode.CHANNELING))
                {

                    if (IsState(ETriggerRunMode.ASYNCHRONOUSLY))
                    {
                        if (!UpdateAsyn())
                        {
                            Continue();
                        }
                    }
                    else
                    {
                        if (!UpdateSyn())
                        {
                            Continue();
                        }
                    }


                }
                if (IsAllActionsExecuted())
                {
                    End();
                }
            }
        }

        private void Continue()
        {
            var isUsing = IsAllTransmitter();

            if (isUsing)
            {
                if (IsState(ETriggerStates.PAUSE))
                {
                    Continue(whoTriggered);
                }
            }
            else
            {

                Pause();
            }
        }

        private bool IsAllTransmitter()
        {
            bool ret = true;
            for (int i = 0; i < transmitters.Count; i++)
            {
                ret = transmitters[i].IsUsing();
                if (!ret) break;
            }
            return ret;
        }
    }
}
