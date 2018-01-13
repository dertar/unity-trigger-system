using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Trigger
{
    /*
     * \brief Action который меняет местами объекты А и Б
     */
    public class ActionSwap : Action
    {
        [Tooltip("Объект А")]
        public GameObject aObject;
        [Tooltip("Объект B")]
        public GameObject bObject;

        void Start()
        {
            enabled = false;
            if (aObject == null || bObject == null)
            {
                Debug.LogError("Object(s) not initialized");
            }
        }

        public override void Update()
        {
            End();
        }

        public override void End()
        {
            SetState(ETriggerStates.FINISHED);
        }

        override public void Run(GameObject obj)
        {
            SwapPosition(aObject.transform, bObject.transform);
            SetState(ETriggerStates.FINISHED);
        }

        private void SwapPosition(Transform a, Transform b)
        {
            Log("swapped positions between gameobjects: " + a.position + "->" + b.position);
            Vector3 tmp = a.position;
            a.position = b.position;
            b.position = tmp;
        }
    }
}