using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Trigger
{
    public class ActionDestroyObject : Action
    {
        public GameObject specificObject;
        public float time;
        public override void End()
        {
            SetState(ETriggerStates.FINISHED);
        }

        public override void Run(GameObject obj)
        {
            if (specificObject == null)
                specificObject = gameObject;

            Destroy(specificObject, time);
        }

        public override void Update()
        {
            End();
        }
    }
}
