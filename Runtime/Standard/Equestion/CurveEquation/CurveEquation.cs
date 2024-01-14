using System;
using UnityEngine;

namespace And.Math.Standard
{
    [Serializable]
    public struct CurveEquation : IEquation<float>
    {
        public float amplitude;
        public float length;
        public AnimationCurve curve;

        public CurveEquation(float amplitude, float length, AnimationCurve curve)
        {
            this.amplitude = amplitude;
            this.length = length;
            this.curve = curve;
        }

        public float Evaluate(float variable)
        {
            return curve.Evaluate(variable / length) * amplitude;
        }
    }
}