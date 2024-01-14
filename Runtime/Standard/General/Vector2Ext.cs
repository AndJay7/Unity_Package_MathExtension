
using UnityEngine;

namespace And.Math.Standard
{
    public static class Vector2Ext
    {
        public static Range ToRange(this Vector2 value)
        {
            return new Range(value);
        }

        public static bool InRange(this Vector2 vec, float min, float max)
        {
            return InRange(vec,new Range(min,max));
        }

        public static bool InRange(this Vector2 vec, Range range)
        {
            bool inX = range.Contains(vec.x);
            bool inY = range.Contains(vec.y);
            return inX && inY;
        }

        public static bool InRange(this Vector2 vec, Vector2 min, Vector2 max)
        {
            bool inMin = vec.x >= min.x && vec.y >= min.y;
            bool inMax = vec.x <= max.x && vec.y <= max.y;
            return inMin && inMax;
        }

        public static Vector2 Abs(this Vector2 value)
        {
            value.x = Mathf.Abs(value.x);
            value.y = Mathf.Abs(value.y);

            return value;
        }

        public static Vector2 Sign(this Vector2 value)
        {
            value.x = Mathf.Sign(value.x);
            value.y = Mathf.Sign(value.y);

            return value;
        }
    }
}