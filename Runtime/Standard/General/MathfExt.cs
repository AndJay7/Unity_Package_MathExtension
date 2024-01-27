using UnityEngine;

namespace And.Math.Standard
{
    public static partial class MathfExt
    {
        //polynomial smoothing by reducing output value, when a and b are close
        public static float SmoothMin(float a, float b, float range)
        {
            if(range == 0)
                return Mathf.Min(a, b);

            float delta = Mathf.Abs(a - b);
            delta = Mathf.Max(range - delta, 0);
            float output = Mathf.Min(a, b) - delta * delta / range * 0.25f;

            return output;
        }

        public static Vector2 SmoothMin(Vector2 a, Vector2 b, Vector2 range)
        {
            Vector2 output;
            output.x = SmoothMin(a.x,b.x, range.x);
            output.y = SmoothMin(a.y,b.y, range.y);
            return output;
        }

        public static Vector3 SmoothMin(Vector3 a, Vector3 b, Vector3 range)
        {
            Vector3 output;
            output.x = SmoothMin(a.x, b.x, range.x);
            output.y = SmoothMin(a.y, b.y, range.y);
            output.z = SmoothMin(a.z, b.z, range.z);
            return output;
        }

        public static float SmoothMax(float a, float b, float range)
        {
            return -SmoothMin(-a,-b,range);
        }

        public static Vector2 SmoothMax(Vector2 a, Vector2 b, Vector2 range)
        {
            return -SmoothMin(-a, -b, range); 
        }

        public static Vector3 SmoothMax(Vector3 a, Vector3 b, Vector3 range)
        {
            return -SmoothMin(-a, -b, range);
        }

        public static float Remap(float value, Range inMinMax, Range outMinMax)
        {
            float output = value;
            output = inMinMax.Remap01(output);
            output = outMinMax.Lerp(output);
            return output;
        }

        public static float Remap(float value, Vector2 inMinMax, Vector2 outMinMax)
        {
            return Remap(value,inMinMax.ToRange(),outMinMax.ToRange());
        }

        public static float SmoothLinear(float current, float target, float speed, float deltaTime)
        {
            float followStep = speed * deltaTime;
            float diff = current - target;
            float output;

            if (Mathf.Abs(diff) <= followStep)
                output = target;
            else
                output = current - (followStep * Mathf.Sign(diff));
            return output;
        }

        public static Quaternion SmoothDamp(Quaternion rot, Quaternion target, ref Quaternion deriv, float time, float maxSpeed, float deltaTime)
        {
            if (Time.deltaTime < Mathf.Epsilon)
                return rot;

            // account for double-cover
            float Dot = Quaternion.Dot(rot, target);
            float Multi = Dot > 0f ? 1f : -1f;
            target.x *= Multi;
            target.y *= Multi;
            target.z *= Multi;
            target.w *= Multi;

            // smooth damp (nlerp approx)
            Vector4 Result = new Vector4(
                Mathf.SmoothDamp(rot.x, target.x, ref deriv.x, time, maxSpeed, deltaTime),
                Mathf.SmoothDamp(rot.y, target.y, ref deriv.y, time, maxSpeed, deltaTime),
                Mathf.SmoothDamp(rot.z, target.z, ref deriv.z, time, maxSpeed, deltaTime),
                Mathf.SmoothDamp(rot.w, target.w, ref deriv.w, time, maxSpeed, deltaTime)
            ).normalized;

            // ensure deriv is tangent
            Vector4 derivError = Vector4.Project(new Vector4(deriv.x, deriv.y, deriv.z, deriv.w), Result);
            deriv.x -= derivError.x;
            deriv.y -= derivError.y;
            deriv.z -= derivError.z;
            deriv.w -= derivError.w;

            return new Quaternion(Result.x, Result.y, Result.z, Result.w);
        }

        public static float ZeroZone(float value, float range)
        {
            return ZeroZone(value, range, range);
        }

        public static float ZeroZone(float value, Range range)
        {
            return ZeroZone(value, range.max, range.min);
        }

        public static float ZeroZone(float value, float positiveRange, float negativeRange)
        {
            float output = value;
            float sign = Mathf.Sign(output);
            float range = sign == 1 ? negativeRange : positiveRange;

            output = Mathf.Abs(output);
            output = Mathf.Clamp(output - range, 0, output);
            output *= sign;

            return output;
        }

    }
}