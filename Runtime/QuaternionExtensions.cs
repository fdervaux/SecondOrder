using UnityEngine;

namespace Package.SecondOrder.Runtime
{
    public static class QuaternionExtensions
    {
        /// <summary>
        /// Adds two quaternions together.
        /// </summary>
        /// <param name="q1"> The first quaternion.</param>
        /// <param name="q2"> The second quaternion.</param>
        /// <returns> q1 + q2 </returns>
        public static Quaternion Add(this Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1.x + q2.x, q1.y + q2.y, q1.z + q2.z, q1.w + q2.w);
        }

        /// <summary>
        /// Subtracts one quaternion from another.
        /// </summary>
        /// <param name="q1"> The first quaternion.</param>
        /// <param name="q2"> The second quaternion.</param>
        /// <returns> q1 - q2 </returns>
        public static Quaternion Subtract(this Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1.x - q2.x, q1.y - q2.y, q1.z - q2.z, q1.w - q2.w);
        }

        /// <summary>
        /// Multiplies a quaternion by a scalar (scale).
        /// </summary>
        /// <param name="q"> The quaternion.</param>
        /// <param name="scale"> The scalar value.</param>
        /// <returns> q * scale </returns>
        public static Quaternion Multiply(this Quaternion q, float scale)
        {
            return new Quaternion(q.x * scale, q.y * scale, q.z * scale, q.w * scale);
        }

        /// <summary>
        /// Multiplies a scalar (scale) by a quaternion.
        /// </summary>
        /// <param name="q"> The quaternion.</param>
        /// <param name="scale"> The scalar value.</param>
        /// <returns> q * scale </returns>
        public static Quaternion Multiply(this float scale, Quaternion q)
        {
            return q.Multiply(scale);
        }

        /// <summary>
        /// Divides a quaternion by a scalar (scale).
        /// </summary>
        /// <param name="q"> The quaternion.</param>
        /// <param name="scale"> The scalar value.</param>
        /// <returns> q / scale </returns>
        public static Quaternion Divide(this Quaternion q, float scale)
        {
            if (scale == 0f)
                return Quaternion.identity; // or handle division by zero as needed

            float inverseScale = 1f / scale;
            return new Quaternion(q.x * inverseScale, q.y * inverseScale, q.z * inverseScale, q.w * inverseScale);
        }

        /// <summary>
        /// Normalizes a quaternion.
        /// </summary>
        /// <param name="q"> The quaternion to normalize.</param>
        /// <returns> Normalized quaternion.</returns>
        public static Quaternion NormalizeQuaternion(this Quaternion q)
        {
            float mag = Mathf.Sqrt(q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w);
            if (mag > 0f)
            {
                return new Quaternion(q.x / mag, q.y / mag, q.z / mag, q.w / mag);
            }

            return Quaternion.identity;
        }

        /// <summary>
        /// Ensures that the quaternion is in the same hemisphere as the reference quaternion.
        /// </summary>
        /// <param name="q"> The quaternion to check.</param>
        /// <param name="reference"> The reference quaternion.</param>
        /// <returns> The quaternion in the same hemisphere as the reference.</returns>
        public static Quaternion EnsureSameHemisphere(this Quaternion q, Quaternion reference)
        {
            return Quaternion.Dot(reference, q) < 0f
                ? new Quaternion(-q.x, -q.y, -q.z, -q.w)
                : q;
        }
    }
}