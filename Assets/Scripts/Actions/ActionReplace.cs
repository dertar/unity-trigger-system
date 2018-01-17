using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Trigger
{
    /*
     * \brief Action который заменяет объект А на Б.
     */
    public class ActionReplace : Action
    {
        [Header("Объект А")]
        public GameObject anObject;
        [Header("Объект Б, он будет заменен")]
        public GameObject beReplacedObject;

        public bool parent = false;

        void Start()
        {
            enabled = false;
            if (anObject == null || beReplacedObject == null)
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
            Log(anObject.name + " replaced " + beReplacedObject);

            GameObject createdObject = Instantiate(anObject, beReplacedObject.transform.position,
                                        beReplacedObject.transform.rotation,
                                        parent ? anObject.transform : null);

            DestroyObject(beReplacedObject);

            createdObject.SetActive(true);
            End();
        }
    }
}
