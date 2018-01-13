using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

namespace Trigger
{
    public class ActionGiveItem : Action
    {
        [SerializeField]
        public Item item;

        override public void Run(GameObject obj)
        {
            Subject character = obj.GetComponent<Subject>();

            if (character == null)
                return;

            if (character.CanPickUp(item))
            {
                Log("Item can be picked up");
                if (character.GiveItem(item))
                {
                    End();
                    Log("Item transferred");
                }
                return;
            }
            SetState(ETriggerStates.PAUSE);
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
