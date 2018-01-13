using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Trigger
{
    /*
     * Action который включает\отключает объект.
     * 		переключатель:
     *           если включен  -> отключит
     * 			 если отключен -> включит
     * */
    public class ActionToggle : Action
    {
        [Tooltip("Объект, который будет переключен")]
        public GameObject certainObj;

        void Start()
        {
            if (certainObj == null)
            {
                Debug.Log("game object not inited");
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

        public override void Run(GameObject obj)
        {
            Toggle(certainObj != null ? certainObj : gameObject);

            End();
            Log("Object toggled");
        }

        private void Toggle(GameObject willToggle)
        {
            willToggle.SetActive(!willToggle.activeSelf);
        }
    }
}
