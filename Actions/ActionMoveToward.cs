using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Trigger
{
    public class ActionMoveToward : ActionMovement
    {
        public GameObject moveTo;
        protected Vector3[] steps;
        protected int iStep;
        protected override void UpdateNextStep()
        {
            if (debug) Debug.DrawLine(transform.position, steps[iStep], Color.cyan);

            transform.position = Vector3.MoveTowards(transform.position, steps[iStep], Time.deltaTime * speed);
        }

        override protected void ProccessSteps()
        {
            //steps.Clear ();

            int count = Mathf.CeilToInt(1f / step) + 1/*- 1*/;
            steps = new Vector3[count];

            float cut = 0f;
            for (int i = 0; i < count; i++, cut += step)
            {
                steps[i] = Vector3.Lerp(transform.position, moveTo.transform.position, cut);
            }
            iStep = 0;
            SetState(ETriggerMovementStates.FORWARD);
            Log("Calculated Steps " + (steps.Length - 1));

            speed = CalculateSpeed(Vector3.Distance(steps[0], steps[count - 1]), time);
            Log("Speed " + speed);

        }

        override protected bool MoveNext()
        {
            if (IsReachedStep())
            {
                iStep++;
            }

            return iStep < steps.Length;
        }
        override protected bool MoveBack()
        {
            if (IsReachedStep())
            {
                Log("back segment");
                return false;
            }

            return iStep >= 0;
        }

        override protected bool IsReachedStep()
        {
            return /*iStep <= 0 && iStep > steps.Length && */
                transform.position == steps[iStep];
        }

        override protected void BoundStep()
        {
            if (iStep < 0) iStep = 0;
            else if (iStep > steps.Length) iStep = steps.Length - 1;
        }

        override protected void SwitchDirection()
        {
            if (IsState(ETriggerMovementStates.FORWARD))
            {
                SetState(ETriggerMovementStates.BACKWARD);
                iStep--;
            }
            else if (IsState(ETriggerMovementStates.BACKWARD))
            {
                SetState(ETriggerMovementStates.FORWARD);
                iStep++;
            }
            BoundStep();
            Log("Switched dir");
        }
    }
}