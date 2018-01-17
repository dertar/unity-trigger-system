using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Trigger
{
    /*
     * \brief Action который запускает анимацию
     * \todo скрипт требуется дописать, для более 
     *       гибкой настройки параметров анимации
     */
    public class ActionAnimation : Action
    {
        ///< Объект на котором есть Animator
        [Header("Объект на котором есть Animator")]
        //[Tooltip("Объект на котором есть Animator")]
        public GameObject gObject;
        [System.Serializable]
        public struct AnimationBoolKeys
        {
            [Tooltip("Название анимации")]
            public string name;
            [Tooltip("Ключ")]
            public bool key;
        }

        [SerializeField]
        public AnimationBoolKeys[] animationParams;
        private Animator animator;
        void Start()
        {
            if (gObject == null)
            {
                Debug.LogError("GameObject not initiliazed");
                return;
            }

            if (animationParams.Length == 0)
            {
                Debug.LogError("animationName not initiliazed");
                return;
            }
            animator = gObject.GetComponent<Animator>();
            enabled = false;
        }


        public override void Update()
        {
            End();
        }

        public override void Run(GameObject obj)
        {
            if (animator == null)
            {
                Debug.LogError("Can't find animator");
                Log("Can't find animator");
                return;
            }

            Log("Preps params for animation");

            for (int i = 0; i < animationParams.Length; i++)
            {
                animator.SetBool(animationParams[i].name, animationParams[i].key);
            }
            enabled = true;
        }

        public override void End()
        {
            SetState(ETriggerStates.FINISHED);
        }
        /*override public void Pause()
        {
            enabled = false;
        }*/
    }
}
