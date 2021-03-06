using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * \brief Триггер, который запускается 
 *        при вхождении колайдера в зону
 */
namespace Trigger
{
    public class EventOnZone : Event
    {
        [Header("Разрешенные слои")]
        [Tooltip("Разрешенные слои для активации триггера")]
        [SerializeField]
        public AllowedLayers layers;

        ///< Список колайдеров, которые находятся в зоне
        protected List<Collider> enteredCollider;

        override protected void Init()
        {
            enteredCollider = new List<Collider>();
            enabled = false;
            countTriggered = 0;
        }

        /*
         *	\brief  Если колайдер зашел в зону, то
         *          добавлет в список enteredCollider
         *          и запускает триггер.
         *  \param[in] col - Collider
         */
        void OnTriggerEnter(Collider col)
        {
            Log ("Collider entered");
            if (layers.IsAllowed(col.gameObject.layer))
            {
                enteredCollider.Add(col);
                Run(col.gameObject);
                Log("Access allowed");
            }
        }

        /*
         *	\brief  Если колайдер вышел из зоны, то
         *          удаляет из списка enteredCollider
         *  \param[in] col - Collider
         */
        void OnTriggerExit(Collider col)
        {
            //if (layers.IsAllowed (col.gameObject.layer))
            //{
            enteredCollider.Remove(col);


            Out(col.gameObject);

            if (enteredCollider.Count == 0)
            {
                enabled = !IsAllActionsExecuted();
                Log("Update switched off");
            }
            Log("Leaved");
            //}
        }

        override public void Update()
        {
            if (IsState(ETriggerStates.RUNNING))
            {
                if (IsState(ETriggerRunMode.SYNCHRONOUSLY))
                {
                    UpdateSyn();
                }
                else
                {
                    UpdateAsyn();
                }
            }
        }
        public void Out(GameObject obj)
        {
            if (!IsAllActionsExecuted())
            {
                if (IsState(ETriggerWorkMode.CHANNELING))
                {
                    Pause();
                }
            }
        }


        override public void End()
        {
            SetState(ETriggerStates.FINISHED);
        }

    }
}
