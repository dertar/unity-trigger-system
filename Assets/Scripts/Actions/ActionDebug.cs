using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Trigger
{
    /*
     * \brief Action который выводить определенное сообщение
     */
    public class ActionDebug : Action
    {
        [Header("Сообщение")]
        public string msg;

        override public void Run(GameObject obj)
        {
            Debug.Log(msg);
            End();
        }

        void Start()
        {
            enabled = false;
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