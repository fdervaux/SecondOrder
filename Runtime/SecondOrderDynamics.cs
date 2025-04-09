
using Plugins.SecondOrder.Runtime;
using UnityEngine;

/// <summary>
/// Second order dynamics class helpers.
/// </summary>
public static class SecondOrderDynamics
{
    /// <summary>
    /// Update the second order dynamics with the given target position and second order data.
    /// </summary>
    /// <param name="targetPosition"> The target position.</param>
    /// <param name="secondOrder"> the second order data.</param>
    /// <param name="deltaTime"> The delta time.</param>
    /// <returns> new position.</returns>
    public static Vector2 SecondOrderUpdate(Vector2 targetPosition, SecondOrder<Vector2> secondOrder, float deltaTime)
    {
        if (!secondOrder.IsInit)
            secondOrder.Init(targetPosition);

        Vector2 xd = (targetPosition - secondOrder.LastPosition) / deltaTime;
        secondOrder.LastPosition = targetPosition;

        return SecondOrderUpdate(targetPosition, xd, secondOrder, deltaTime);
    }
    
    /// <summary>
    /// Update the second order dynamics with the given target position and second order data.
    /// </summary>
    /// <param name="targetPosition"> The target position.</param>
    /// <param name="targetVelocity"> The target velocity.</param>
    /// <param name="secondOrder"> the second order data.</param>
    /// <param name="deltaTime"> The delta time.</param>
    /// <returns> new position.</returns>
    public static Vector2 SecondOrderUpdate(Vector2 targetPosition, Vector2 targetVelocity, SecondOrder<Vector2> secondOrder, float deltaTime)
    {
        if (!secondOrder.IsInit)
            secondOrder.Init(targetPosition);

        secondOrder.Data.SetDeltaTime(deltaTime);

        if (secondOrder.IsInit)
        {
            secondOrder.TargetPosition = targetPosition;
            secondOrder.LastPosition = targetPosition;
        }

        secondOrder.TargetPosition = secondOrder.TargetPosition + deltaTime * secondOrder.TargetVelocity;
        secondOrder.TargetVelocity = secondOrder.TargetVelocity + deltaTime * (targetPosition + secondOrder.Data.K3 * targetVelocity - secondOrder.TargetPosition - secondOrder.Data.K1 * secondOrder.TargetVelocity) / secondOrder.Data.K2Stable;

        return secondOrder.TargetPosition;
    }

    /// <summary>
    /// Update the second order dynamics with the given target position and second order data.
    /// </summary>
    /// <param name="targetPosition"> The target position.</param>
    /// <param name="secondOrder"> the second order data.</param>
    /// <param name="deltaTime"> The delta time.</param>
    /// <returns> new position.</returns>
    public static Vector3 SecondOrderUpdate(Vector3 targetPosition, SecondOrder<Vector3> secondOrder, float deltaTime)
    {
        if (!secondOrder.IsInit)
            secondOrder.Init(targetPosition);

        Vector3 xd = (targetPosition - secondOrder.LastPosition) / deltaTime;
        secondOrder.LastPosition = targetPosition;

        return SecondOrderUpdate(targetPosition, xd, secondOrder, deltaTime);
    }
    
    /// <summary>
    /// Update the second order dynamics with the given target position and second order data.
    /// </summary>
    /// <param name="targetPosition"> The target position.</param>
    /// <param name="targetVelocity"> The target velocity.</param>
    /// <param name="secondOrder"> the second order data.</param>
    /// <param name="deltaTime"> The delta time.</param>
    /// <returns> new position.</returns>
    public static Vector3 SecondOrderUpdate(Vector3 targetPosition, Vector3 targetVelocity, SecondOrder<Vector3> secondOrder, float deltaTime)
    {
        if (!secondOrder.IsInit)
            secondOrder.Init(targetPosition);

        secondOrder.Data.SetDeltaTime(deltaTime);

        secondOrder.TargetPosition = secondOrder.TargetPosition + deltaTime * secondOrder.TargetVelocity;
        secondOrder.TargetVelocity = secondOrder.TargetVelocity + deltaTime * (targetPosition + secondOrder.Data.K3 * targetVelocity - secondOrder.TargetPosition - secondOrder.Data.K1 * secondOrder.TargetVelocity) / secondOrder.Data.K2Stable;

        return secondOrder.TargetPosition;
    }

    /// <summary>
    /// Update the second order dynamics with the given target position and second order data.
    /// </summary>
    /// <param name="targetPosition"> The target position.</param>
    /// <param name="secondOrder"> the second order data.</param>
    /// <param name="deltaTime"> The delta time.</param>
    /// <returns> new position.</returns>
    public static float SecondOrderUpdate(float targetPosition, SecondOrder<float> secondOrder, float deltaTime)
    {
        if (!secondOrder.IsInit)
            secondOrder.Init(targetPosition);

        float xd = (targetPosition - secondOrder.LastPosition) / deltaTime;
        secondOrder.LastPosition = targetPosition;

        return SecondOrderUpdate(targetPosition, xd, secondOrder, deltaTime);
    }
    
    /// <summary>
    /// Update the second order dynamics with the given target position and second order data.
    /// </summary>
    /// <param name="targetPosition"> The target position.</param>
    /// <param name="targetVelocity"> The target velocity.</param>
    /// <param name="secondOrder"> the second order data.</param>
    /// <param name="deltaTime"> The delta time.</param>
    /// <returns> new position.</returns>
    public static float SecondOrderUpdate(float targetPosition, float targetVelocity, SecondOrder<float> secondOrder, float deltaTime)
    {
        if (!secondOrder.IsInit)
            secondOrder.Init(targetPosition);

        secondOrder.Data.SetDeltaTime(deltaTime);

        secondOrder.TargetPosition = secondOrder.TargetPosition + deltaTime * secondOrder.TargetVelocity;
        secondOrder.TargetVelocity = secondOrder.TargetVelocity + deltaTime * (targetPosition + secondOrder.Data.K3 * targetVelocity - secondOrder.TargetPosition - secondOrder.Data.K1 * secondOrder.TargetVelocity) / secondOrder.Data.K2Stable;

        return secondOrder.TargetPosition;
    }

    /// <summary>
    /// Update the second order dynamics with the given target rotation and second order data.
    /// </summary>
    /// <param name="targetRotation"> The target rotation.</param>
    /// <param name="secondOrder"> the second order data.</param>
    /// <param name="deltaTime"> The delta time.</param>
    /// <returns> new rotation.</returns>
    public static Quaternion SecondOrderUpdate(Quaternion targetRotation, SecondOrder<Quaternion> secondOrder, float deltaTime)
    {
        if (!secondOrder.IsInit)
            secondOrder.Init(targetRotation);

        Quaternion targetVelocity;

        targetVelocity.x = (targetRotation.x - secondOrder.LastPosition.x) / deltaTime;
        targetVelocity.y = (targetRotation.y - secondOrder.LastPosition.y) / deltaTime;
        targetVelocity.z = (targetRotation.z - secondOrder.LastPosition.z) / deltaTime;
        targetVelocity.w = (targetRotation.w - secondOrder.LastPosition.w) / deltaTime;

        secondOrder.LastPosition = targetRotation;

        return SecondOrderUpdate(targetRotation, targetVelocity, secondOrder, deltaTime);
    }

    /// <summary>
    /// Normalize the sign of the quaternion.
    /// </summary>
    /// <param name="current"> The current quaternion.</param>
    /// <param name="target"> The target quaternion.</param>
    /// <returns></returns>
    private static bool NormalizeSign(Quaternion current, ref Quaternion target)
    {
        // if our dot product is positive, we don't need to invert signs.
        if (Quaternion.Dot(current, target) >= 0)
        {
            return false;
        }

        // invert the signs on the components
        target.x *= -1;
        target.y *= -1;
        target.z *= -1;
        target.w *= -1;

        return true;
    }

    /// <summary>
    /// Update the second order dynamics with the given target rotation and second order data.
    /// </summary>
    /// <param name="targetRotation"> The target rotation.</param>
    /// <param name="targetVelocity"> The target velocity.</param>
    /// <param name="secondOrder"> the second order data.</param>
    /// <param name="deltaTime"> The delta time.</param>
    /// <returns> new rotation.</returns>
    public static Quaternion SecondOrderUpdate(Quaternion targetRotation, Quaternion targetVelocity, SecondOrder<Quaternion> secondOrder, float deltaTime)
    {
        if (!secondOrder.IsInit)
            secondOrder.Init(targetRotation);

        secondOrder.Data.SetDeltaTime(deltaTime);

        //bool inverted = NormalizeSign(secondOrder.targetPosition, ref targetRotation);

        Quaternion tp;

        tp.x = secondOrder.TargetPosition.x + deltaTime * secondOrder.TargetVelocity.x;
        tp.y = secondOrder.TargetPosition.y + deltaTime * secondOrder.TargetVelocity.y;
        tp.z = secondOrder.TargetPosition.z + deltaTime * secondOrder.TargetVelocity.z;
        tp.w = secondOrder.TargetPosition.w + deltaTime * secondOrder.TargetVelocity.w;

        secondOrder.TargetPosition = tp;

        Quaternion tv;

        tv.x = secondOrder.TargetVelocity.x + deltaTime * (targetRotation.x + secondOrder.Data.K3 * targetVelocity.x - secondOrder.TargetPosition.x - secondOrder.Data.K1 * secondOrder.TargetVelocity.x) / secondOrder.Data.K2Stable;
        tv.y = secondOrder.TargetVelocity.y + deltaTime * (targetRotation.y + secondOrder.Data.K3 * targetVelocity.y - secondOrder.TargetPosition.y - secondOrder.Data.K1 * secondOrder.TargetVelocity.y) / secondOrder.Data.K2Stable;
        tv.z = secondOrder.TargetVelocity.z + deltaTime * (targetRotation.z + secondOrder.Data.K3 * targetVelocity.z - secondOrder.TargetPosition.z - secondOrder.Data.K1 * secondOrder.TargetVelocity.z) / secondOrder.Data.K2Stable;
        tv.w = secondOrder.TargetVelocity.w + deltaTime * (targetRotation.w + secondOrder.Data.K3 * targetVelocity.w - secondOrder.TargetPosition.w - secondOrder.Data.K1 * secondOrder.TargetVelocity.w) / secondOrder.Data.K2Stable;

        secondOrder.TargetVelocity = tv;

        return secondOrder.TargetPosition;
    }


}
