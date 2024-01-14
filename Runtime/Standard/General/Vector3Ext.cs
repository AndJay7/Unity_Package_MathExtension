using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace And.Math.Standard
{
    public static class Vector3Ext
    {
        public static Vector2 ToVectorXZ(this Vector3 value)
        {
            return new Vector2(value.x, value.z);
        }

        public static bool InRange(this Vector3 value, float min, float max)
        {
            bool inMin = value.x >= min && value.y >= min && value.z >= min;
            bool inMax = value.x <= max && value.y <= max && value.z <= max;
            return inMin && inMax;
        }

        public static Vector3 Abs(this Vector3 value)
        {
            value.x = Mathf.Abs(value.x);
            value.y = Mathf.Abs(value.y);
            value.z = Mathf.Abs(value.z);

            return value;
        }
    }
}