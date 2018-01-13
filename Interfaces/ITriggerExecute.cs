using UnityEngine;
namespace Trigger
{
    interface ITriggerExecute
    {
        void Run(GameObject whoTriggered);
        void Update();
        void End();

        void Pause();
        void Continue(GameObject whoContinue);
    }
}