using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Trigger
{
    /*
     * \brief Action который останавливает выполнения ParticleSystem
     */

    public class ActionToggleParticle : Action
    {

        public ParticleSystem particle;
        [Header("Остановить полностью или поставить на паузу")]
        public bool pause = false;

        void Start()
        {
            enabled = false;
            if (particle == null)
            {
                Debug.LogError(name + ": particle not init");
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
            if (particle.isStopped || particle.isPaused)
            {
                particle.Play();
                Log("Particle play");
            }
            else if (!pause)
            {
                particle.Stop();
                Log("Particle stop");
            }
            else
            {
                particle.Pause();
                Log("Particle pause");
            }
            End();
        }
    }
}