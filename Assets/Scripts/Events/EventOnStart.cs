using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Trigger
{
    public class EventOnStart : Event
    {

        override protected void Init()
        {
            enabled = true;
            countTriggered = 0;
        }

        override public void Update()
        {
            Run(this.gameObject);
            End();
        }

        override public void End()
        {
            SetState(ETriggerStates.FINISHED);
            Log("OnStart.executed");
        }
    }
}
