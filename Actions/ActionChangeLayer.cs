using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Trigger
{
    public class ActionChangeLayer : Action
    {
        public int layer;
        public GameObject gObject;

        public void Start()
        {
            if (gObject == null)
            {
                gObject = gameObject;
            }
        }

        override public void Run(GameObject obj)
        {
            Log("Changing layer from " + gObject.layer + " to " + layer);
            gObject.layer = layer;
            End();
        }

        public override void Update()
        {
            End();
        }

        public override void End()
        {
            SetState(ETriggerStates.FINISHED);
        }
    }
}
