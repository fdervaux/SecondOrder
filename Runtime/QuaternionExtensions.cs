using UnityEngine;

namespace Packages.SecondOrder.Runtime
{
    public static class QuaternionExtensions
    {
        // Addition: q1 + q2
        public static Quaternion Add(this Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1.x + q2.x, q1.y + q2.y, q1.z + q2.z, q1.w + q2.w);
        }

        // Subtraction: q1 - q2
        public static Quaternion Subtract(this Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1.x - q2.x, q1.y - q2.y, q1.z - q2.z, q1.w - q2.w);
        }

        // Multiplication: q * scalar (scale)
        public static Quaternion Multiply(this Quaternion q, float scale)
        {
            return new Quaternion(q.x * scale, q.y * scale, q.z * scale, q.w * scale);
        }

        // Multiplication: scalar (scale) * q
        public static Quaternion Multiply(this float scale, Quaternion q)
        {
            return q.Multiply(scale);
        }

        // Division: q / scalar (scale)
        public static Quaternion Divide(this Quaternion q, float scale)
        {
            if (scale == 0f)
                return Quaternion.identity; // or handle division by zero as needed

            float inverseScale = 1f / scale;
            return new Quaternion(q.x * inverseScale, q.y * inverseScale, q.z * inverseScale, q.w * inverseScale);
        }

        public static Quaternion NormalizeQuaternion(this Quaternion q)
        {
            float mag = Mathf.Sqrt(q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w);
            if (mag > 0f)
            {
                return new Quaternion(q.x / mag, q.y / mag, q.z / mag, q.w / mag);
            }

            return Quaternion.identity;
        }

        public static Quaternion EnsureSameHemisphere(this Quaternion q, Quaternion reference)
        {
            return Quaternion.Dot(reference, q) < 0f
                ? new Quaternion(-q.x, -q.y, -q.z, -q.w)
                : q;
        }
    }
}