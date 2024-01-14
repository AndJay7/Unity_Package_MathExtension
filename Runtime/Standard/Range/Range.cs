using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace And.Math.Standard
{
    [Serializable]
    public struct Range
    {
        public float min;

        public float max;

        public float Middle => (min + max) / 2;
        public float Lenght => max - min;

        public Range(float start, float end)
        {
            this.min = start;
            this.max = end;
        }

        public Range(Vector2 vector)
        {
            min = vector.x;
            max = vector.y;
        }

        public float Remap01(float t, bool clamped = false)
        {
            // remap value from Start-End range to 0-1 range
            float value = (t - min) / (max - min);
            if (clamped)
                value = Clamp(value, 0, 1);
            return value;
        }

        public float Evaluate(float t, bool clamped = false)
        {
            if (clamped)
                t = Clamp(t, 0, 1);
            return (max - min) * t + min;
        }

        public bool Contains(float value, bool startOpen = true, bool endOpen = true)
        {
            bool startCheck;
            bool endCheck;

            if (startOpen)
                startCheck = value >= min;
            else
                startCheck = value > min;

            if (endOpen)
                endCheck = value <= max;
            else
                endCheck = value < max;

            return startCheck && endCheck; 
        }

        public void Encapsulate(float value)
        {
            min = Mathf.Min(min, value);
            max = Mathf.Max(max, value);
        }

        public void Encapsulate(Range range)
        {
            min = Mathf.Min(min, range.min);
            max = Mathf.Max(max, range.max);
        }

        public void Intersect(Range range)
        {
            min = Mathf.Max(min, range.min);
            max = Mathf.Min(max, range.max);
        }

        public float Clamp(float value, float start, float end)
        {            
            return Mathf.Clamp(value,start, end);
        }

        public static Range Lerp(Range startRange, Range endRange, float t)
        {
            Range range = new Range();
            range.min = Mathf.Lerp(startRange.min, endRange.min, t);
            range.max = Mathf.Lerp(startRange.max, endRange.max, t);
            return range;
        }

        public Vector2 ToVector()
        {
            return new Vector2(min, max);
        }

        public override string ToString()
        {
            return string.Format("({0},{1})",min,max);
        }

        public static Range operator +(Range a, float b)
        {
            a.Encapsulate(b);
            return a;
        }

        public static Range operator +(Range a, Range b)
        {
            a.Encapsulate(b);
            return a;
        }

        public static Range operator *(Range a, Range b)
        {
            a.Intersect(b);
            return a;
        }
    }
}