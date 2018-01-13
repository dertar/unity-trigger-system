using UnityEngine;
namespace Trigger
{
    public interface ITriggerUse
    {
        EUseState Use(GameObject whoUse);
        void Using(GameObject whoUsing);
    }
}