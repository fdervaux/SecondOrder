using System;
using UnityEngine;

namespace Package.SecondOrder.Runtime
{
    public static class SecondOrderDynamics
    {
        /// <summary>
        /// Generic second order update function.
        /// </summary>
        /// <param name="targetPosition"> Target position.</param>
        /// <param name="targetVelocity"> Target velocity.</param>
        /// <param name="secondOrder"> Second order data.</param>
        /// <param name="deltaTime"> Delta time.</param>
        /// <param name="add"> Add function.</param>
        /// <param name="subtract"> Subtract function.</param>
        /// <param name="scale"> Scale function.</param>
        /// <param name="normalize"> Normalize function.</param>
        /// <typeparam name="T"> Type of the target position and velocity.</typeparam>
        /// <returns> Updated position.</returns>
        private static T GenericSecondOrderUpdate<T>(
            T targetPosition, T targetVelocity, SecondOrder<T> secondOrder, float deltaTime,
            Func<T, T, T> add, Func<T, T, T> subtract, Func<T, float, T> scale, Func<T, T> normalize = null)
        {
            if (!secondOrder.IsInit)
                secondOrder.Init(targetPosition);

            if (deltaTime == 0)
                return secondOrder.LastPosition;

            normalize ??= x => x;

            secondOrder.Data.SetDeltaTime(deltaTime);

            
            secondOrder.Position = add(secondOrder.Position, scale(secondOrder.Velocity, deltaTime));

            secondOrder.Velocity = add(secondOrder.Velocity, scale(
                subtract(add(targetPosition, scale(targetVelocity, secondOrder.Data.K3)),
                    add(secondOrder.Position, scale(secondOrder.Velocity, secondOrder.Data.K1Stable))),
                deltaTime / secondOrder.Data.K2Stable));

          
            secondOrder.Position = normalize(secondOrder.Position);

            return secondOrder.Position;
        }

        /// <summary>
        /// Update the second order dynamics for a Vector2 target position.
        /// </summary>
        /// <param name="targetPosition"> Target position.</param>
        /// <param name="secondOrder"> second order data.</param>
        /// <param name="deltaTime"> delta time.</param>
        /// <returns> Updated position.</returns>
        public static Vector2 Update(Vector2 targetPosition, SecondOrder<Vector2> secondOrder,
            float deltaTime)
        {
            if (deltaTime == 0)
                return secondOrder.Position;

            Vector2 velocity = (targetPosition - secondOrder.LastPosition) / deltaTime;
            secondOrder.LastPosition = targetPosition;
            return GenericSecondOrderUpdate(targetPosition, velocity, secondOrder, deltaTime,
                (a, b) => a + b, (a, b) => a - b, (a, s) => a * s);
        }

        /// <summary>
        /// Update the second order dynamics for a Vector3 target position.
        /// </summary>
        /// <param name="targetPosition"> Target position.</param>
        /// <param name="secondOrder"> Second order data.</param>
        /// <param name="deltaTime"> delta time.</param>
        /// <returns> Updated position.</returns>
        public static Vector3 Update(Vector3 targetPosition, SecondOrder<Vector3> secondOrder,
            float deltaTime)
        {
            if (deltaTime == 0)
                return secondOrder.Position;

            Vector3 velocity = (targetPosition - secondOrder.LastPosition) / deltaTime;
            secondOrder.LastPosition = targetPosition;
            return GenericSecondOrderUpdate(targetPosition, velocity, secondOrder, deltaTime,
                (a, b) => a + b, (a, b) => a - b, (a, s) => a * s);
        }

        /// <summary>
        /// Update the second order dynamics for a Vector3 target position.
        /// </summary>
        /// <param name="targetPosition"> Target position.</param>
        /// <param name="secondOrder"> Second order data.</param>
        /// <param name="deltaTime"> delta time.</param>
        /// <returns> Updated position.</returns>
        public static float Update(float targetPosition, SecondOrder<float> secondOrder, float deltaTime)
        {
            if (deltaTime == 0)
                return secondOrder.Position;

            float velocity = (targetPosition - secondOrder.LastPosition) / deltaTime;
            secondOrder.LastPosition = targetPosition;
            return GenericSecondOrderUpdate(targetPosition, velocity, secondOrder, deltaTime,
                (a, b) => a + b, (a, b) => a - b, (a, s) => a * s);
        }

        /// <summary>
        /// Update the second order dynamics for a Vector3 target position.
        /// </summary>
        /// <param name="targetRotation"> Target rotation.</param>
        /// <param name="secondOrder"> Second order data.</param>
        /// <param name="deltaTime"> delta time.</param>
        /// <returns> Updated rotation.</returns>
        public static Quaternion Update(Quaternion targetRotation, SecondOrder<Quaternion> secondOrder,
            float deltaTime)
        {
            if (deltaTime == 0)
                return secondOrder.Position;

            targetRotation = targetRotation.EnsureSameHemisphere(secondOrder.LastPosition);

            Quaternion deltaRotation = targetRotation.Subtract(secondOrder.LastPosition);
            Quaternion velocity = deltaRotation.Divide(deltaTime);

            secondOrder.LastPosition = targetRotation;

            return GenericSecondOrderUpdate(targetRotation, velocity, secondOrder, deltaTime,
                (a, b) => a.Add(b),
                (a, b) => a.Subtract(b),
                (a, s) => a.Multiply(s),
                a => a.NormalizeQuaternion());
        }
    }
}