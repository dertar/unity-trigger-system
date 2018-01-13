using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Trigger
{
    /*
     * \brief Action который запускает AudioSource
     */
    public class ActionSound : Action
    {
        [Tooltip("Звук")]
        public AudioSource audioSource;

        void Start()
        {
            if (audioSource == null)
            {
                Debug.LogError("audio source not initiliazed");
                return;
            }
            enabled = false;
        }

        public override void Update()
        {
            if (!audioSource.isPlaying)
            {
                End();
            }
        }


        public override void End()
        {
            SetState(ETriggerStates.FINISHED);
        }

        override public void Run(GameObject obj)
        {
            Log("playing sound");
            audioSource.Play();
            SetState(ETriggerStates.RUNNING);
        }
    }
}
