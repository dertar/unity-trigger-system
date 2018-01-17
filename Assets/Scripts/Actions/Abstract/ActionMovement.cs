using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Trigger
{
    public abstract class ActionMovement : Action,
                                        ITriggerMovementStates
    {

        protected ETriggerMovementStates move = ETriggerMovementStates.NOT_ACTIVED;
        protected float speed;
        public float time = 10f;

        public float step = 1f;

        public EActionMovementStates type = EActionMovementStates.ONLY_FORWARD;

        abstract protected void UpdateNextStep();
        abstract protected void ProccessSteps();
        abstract protected bool MoveNext();
        abstract protected bool MoveBack();
        abstract protected bool IsReachedStep();
        abstract protected void SwitchDirection();
        abstract protected void BoundStep();

        void Start()
        {
            enabled = false;
        }


        public override void Update()
        {
            if (IsState(ETriggerStates.RUNNING))
            {

                //transform.position = Vector3.MoveTowards (transform.position, steps [iStep], Time.deltaTime * speed);
                UpdateNextStep();

                if (IsState(ETriggerMovementStates.FORWARD))
                {
                    MoveToNextPosition();
                }
                else if (IsState(ETriggerMovementStates.BACKWARD))
                {
                    MoveToBackPosition();
                }

            }
        }

        public override void End()
        {
            SetState(ETriggerStates.FINISHED);
            SetState(ETriggerMovementStates.FINISHED);
        }
        override public void Run(GameObject whoTriggered)
        {
            ProccessSteps();
            MoveToNextPosition();
        }

        override public void Pause()
        {
            Log("Pause detected");
            if (type == EActionMovementStates.ONLY_FORWARD)
            {
                base.Pause();
            }
            else if (type == EActionMovementStates.FORWARD_BACKWARD)
            {
                SwitchDirection();
            }
        }
        override public void Continue(GameObject obj)
        {
            Log("Continue detected");
            if (type == EActionMovementStates.FORWARD_BACKWARD)
            {
                SwitchDirection();
            }
            else if (type == EActionMovementStates.ONLY_FORWARD)
            {
                SetState(ETriggerMovementStates.FORWARD);
            }
            SetState(ETriggerStates.RUNNING);
        }


        private void MoveToNextPosition()
        {
            //Log ("Moving to next pos");
            if (MoveNext()/*it.MoveNext ()*/)
            {
                SetState(ETriggerStates.RUNNING);
            }
            else
            {
                Log("Reached last pos");
                End();
            }
        }

        private void MoveToBackPosition()
        {
            Log("Moving to back pos");
            if (MoveBack())
            {
                SetState(ETriggerStates.RUNNING);
            }
            else
            {
                SetState(ETriggerStates.PAUSE);
            }
        }





        protected float CalculateSpeed(float distance, float time)
        {
            return distance / time;
        }


        public bool IsState(ETriggerMovementStates state)
        {
            return state == move;
        }

        public void SetState(ETriggerMovementStates state)
        {
            move = state;
        }
    }
}
