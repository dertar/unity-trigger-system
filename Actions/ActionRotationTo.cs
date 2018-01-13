using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Trigger
{
    public class ActionRotationTo : ActionRotation
    {
        public float rotation;
        [HeaderAttribute("Нормализованный вектор направления")]
        public Vector3 direction;

        override protected Quaternion RotateTowards()
        {
            return AddDegreesToQuaternion(transform.rotation, direction, rotation);
        }

        private Quaternion AddDegreesToQuaternion(Quaternion to, Vector3 dir, float degrees)
        {
            return to * Quaternion.Euler(degrees * dir.x,
                                         degrees * dir.y,
                                         degrees * dir.z);
        }
    }
}
