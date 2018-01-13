using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Trigger
{
    public class EventOnUseTransmitter : MonoBehaviour,
                                ITriggerUse,
                                ITriggerStates,
                                IDebug
    {
        public bool debug = false;
        public bool cap = false;

        private ETriggerStates state = ETriggerStates.NOT_ACTIVATED;
        private GameObject whoUse;

        public EventOnUse receiver;

        private void Start()
        {
            RegisterTransmitter();
            SetState(ETriggerStates.NOT_ACTIVATED);
        }

        private void Update()
        {
            /*if(pressing)
            {
                SetPressing(false);

                if(IsState(ETriggerStates.PAUSE))
                {
                    receiver.Continue(whoUse);				
                }
            }else
            {
                receiver.Pause();
            }*/
        }

        public void RegisterTransmitter()
        {
            if (receiver == null)
            {
                Debug.LogError("Receiver is null!");
                return;
            }

            receiver.AddTransmitter(this);
        }

        public void StopUse(GameObject whoUse)
        {
            if(whoUse.Equals(this.whoUse))
                this.whoUse = null;
        }

        public EUseState Use(GameObject whoUse)
        {
            if (IsState(ETriggerStates.NOT_ACTIVATED) || IsState(ETriggerStates.PAUSE))
            {
                Using(whoUse);
                receiver.Signal(whoUse);
                enabled = true;
                return EUseState.USING;
            }

            return EUseState.FAILED;
            /*else if (IsState(ETriggerStates.RUNNING))
            {
                //receiver.Signal(whoUse);
                Using(whoUse);
                enabled = true;
                return EUseState.USING;
            }
            else
            {
                whoUse = null;
                SetPressing(false);
                return EUseState.USED;
            }*/
        }

        public void Using(GameObject whoUsing)
        {
            this.whoUse = whoUsing;
        }

        public bool IsUsing()
        {
            return whoUse != null;
        }

        public bool IsState(ETriggerStates state)
        {
            return this.state == state;
        }

        public void SetState(ETriggerStates state)
        {
            this.state = state;
        }

        public void Log(string msg)
        {
            if (debug) Debug.Log(msg);
        }
    }
}
