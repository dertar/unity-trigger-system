using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Trigger
{
    public class ActionRotationToward : ActionRotation
    {
        public GameObject rotateToward;

        override protected Quaternion RotateTowards()
        {
            Quaternion direction = rotateToward.transform.rotation * Quaternion.Inverse(transform.rotation);

            return transform.rotation * direction;
        }
    }
}