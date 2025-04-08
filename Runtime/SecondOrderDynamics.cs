
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
    public static Vector2 SencondOrderUpdate(Vector2 targetPosition, SecondOrder<Vector2> secondOrder, float deltaTime)
    {
        if (!secondOrder.IsInit)
            secondOrder.Init(targetPosition);

        Vector2 xd = (targetPosition - secondOrder.lastPosition) / deltaTime;
        secondOrder.lastPosition = targetPosition;

        return SencondOrderUpdate(targetPosition, xd, secondOrder, deltaTime);
    }
    
    /// <summary>
    /// Update the second order dynamics with the given target position and second order data.
    /// </summary>
    /// <param name="targetPosition"> The target position.</param>
    /// <param name="targetVelocity"> The target velocity.</param>
    /// <param name="secondOrder"> the second order data.</param>
    /// <param name="deltaTime"> The delta time.</param>
    /// <returns> new position.</returns>
    public static Vector2 SencondOrderUpdate(Vector2 targetPosition, Vector2 targetVelocity, SecondOrder<Vector2> secondOrder, float deltaTime)
    {
        if (!secondOrder.IsInit)
            secondOrder.Init(targetPosition);

        secondOrder.Data.setDeltaTime(deltaTime);

        if (secondOrder.IsInit)
        {
            secondOrder.targetPosition = targetPosition;
            secondOrder.lastPosition = targetPosition;
        }

        secondOrder.targetPosition = secondOrder.targetPosition + deltaTime * secondOrder.targetVelocity;
        secondOrder.targetVelocity = secondOrder.targetVelocity + deltaTime * (targetPosition + secondOrder.Data.K3 * targetVelocity - secondOrder.targetPosition - secondOrder.Data.K1 * secondOrder.targetVelocity) / secondOrder.Data.K2_stable;

        return secondOrder.targetPosition;
    }

    /// <summary>
    /// Update the second order dynamics with the given target position and second order data.
    /// </summary>
    /// <param name="targetPosition"> The target position.</param>
    /// <param name="secondOrder"> the second order data.</param>
    /// <param name="deltaTime"> The delta time.</param>
    /// <returns> new position.</returns>
    public static Vector3 SencondOrderUpdate(Vector3 targetPosition, SecondOrder<Vector3> secondOrder, float deltaTime)
    {
        if (!secondOrder.IsInit)
            secondOrder.Init(targetPosition);

        Vector3 xd = (targetPosition - secondOrder.lastPosition) / deltaTime;
        secondOrder.lastPosition = targetPosition;

        return SencondOrderUpdate(targetPosition, xd, secondOrder, deltaTime);
    }
    
    /// <summary>
    /// Update the second order dynamics with the given target position and second order data.
    /// </summary>
    /// <param name="targetPosition"> The target position.</param>
    /// <param name="targetVelocity"> The target velocity.</param>
    /// <param name="secondOrder"> the second order data.</param>
    /// <param name="deltaTime"> The delta time.</param>
    /// <returns> new position.</returns>
    public static Vector3 SencondOrderUpdate(Vector3 targetPosition, Vector3 targetVelocity, SecondOrder<Vector3> secondOrder, float deltaTime)
    {
        if (!secondOrder.IsInit)
            secondOrder.Init(targetPosition);

        secondOrder.Data.setDeltaTime(deltaTime);

        secondOrder.targetPosition = secondOrder.targetPosition + deltaTime * secondOrder.targetVelocity;
        secondOrder.targetVelocity = secondOrder.targetVelocity + deltaTime * (targetPosition + secondOrder.Data.K3 * targetVelocity - secondOrder.targetPosition - secondOrder.Data.K1 * secondOrder.targetVelocity) / secondOrder.Data.K2_stable;

        return secondOrder.targetPosition;
    }

    /// <summary>
    /// Update the second order dynamics with the given target position and second order data.
    /// </summary>
    /// <param name="targetPosition"> The target position.</param>
    /// <param name="secondOrder"> the second order data.</param>
    /// <param name="deltaTime"> The delta time.</param>
    /// <returns> new position.</returns>
    public static float SencondOrderUpdate(float targetPosition, SecondOrder<float> secondOrder, float deltaTime)
    {
        if (!secondOrder.IsInit)
            secondOrder.Init(targetPosition);

        float xd = (targetPosition - secondOrder.lastPosition) / deltaTime;
        secondOrder.lastPosition = targetPosition;

        return SencondOrderUpdate(targetPosition, xd, secondOrder, deltaTime);
    }
    
    /// <summary>
    /// Update the second order dynamics with the given target position and second order data.
    /// </summary>
    /// <param name="targetPosition"> The target position.</param>
    /// <param name="targetVelocity"> The target velocity.</param>
    /// <param name="secondOrder"> the second order data.</param>
    /// <param name="deltaTime"> The delta time.</param>
    /// <returns> new position.</returns>
    public static float SencondOrderUpdate(float targetPosition, float targetVelocity, SecondOrder<float> secondOrder, float deltaTime)
    {
        if (!secondOrder.IsInit)
            secondOrder.Init(targetPosition);

        secondOrder.Data.setDeltaTime(deltaTime);

        secondOrder.targetPosition = secondOrder.targetPosition + deltaTime * secondOrder.targetVelocity;
        secondOrder.targetVelocity = secondOrder.targetVelocity + deltaTime * (targetPosition + secondOrder.Data.K3 * targetVelocity - secondOrder.targetPosition - secondOrder.Data.K1 * secondOrder.targetVelocity) / secondOrder.Data.K2_stable;

        return secondOrder.targetPosition;
    }

    /// <summary>
    /// Update the second order dynamics with the given target rotation and second order data.
    /// </summary>
    /// <param name="targetPosition"> The target rotation.</param>
    /// <param name="secondOrder"> the second order data.</param>
    /// <param name="deltaTime"> The delta time.</param>
    /// <returns> new rotation.</returns>
    public static Quaternion SencondOrderUpdate(Quaternion targetRotation, SecondOrder<Quaternion> secondOrder, float deltaTime)
    {
        if (!secondOrder.IsInit)
            secondOrder.Init(targetRotation);

        Quaternion targetVelocity;

        targetVelocity.x = (targetRotation.x - secondOrder.lastPosition.x) / deltaTime;
        targetVelocity.y = (targetRotation.y - secondOrder.lastPosition.y) / deltaTime;
        targetVelocity.z = (targetRotation.z - secondOrder.lastPosition.z) / deltaTime;
        targetVelocity.w = (targetRotation.w - secondOrder.lastPosition.w) / deltaTime;

        secondOrder.lastPosition = targetRotation;

        return SencondOrderUpdate(targetRotation, targetVelocity, secondOrder, deltaTime);
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
    /// <param name="targetPosition"> The target rotation.</param>
    /// <param name="targetVelocity"> The target velocity.</param>
    /// <param name="secondOrder"> the second order data.</param>
    /// <param name="deltaTime"> The delta time.</param>
    /// <returns> new rotation.</returns>
    public static Quaternion SencondOrderUpdate(Quaternion targetRotation, Quaternion targetVelocity, SecondOrder<Quaternion> secondOrder, float deltaTime)
    {
        if (!secondOrder.IsInit)
            secondOrder.Init(targetRotation);

        secondOrder.Data.setDeltaTime(deltaTime);

        //bool inverted = NormalizeSign(secondOrder.targetPosition, ref targetRotation);

        Quaternion tp;

        tp.x = secondOrder.targetPosition.x + deltaTime * secondOrder.targetVelocity.x;
        tp.y = secondOrder.targetPosition.y + deltaTime * secondOrder.targetVelocity.y;
        tp.z = secondOrder.targetPosition.z + deltaTime * secondOrder.targetVelocity.z;
        tp.w = secondOrder.targetPosition.w + deltaTime * secondOrder.targetVelocity.w;

        secondOrder.targetPosition = tp;

        Quaternion tv;

        tv.x = secondOrder.targetVelocity.x + deltaTime * (targetRotation.x + secondOrder.Data.K3 * targetVelocity.x - secondOrder.targetPosition.x - secondOrder.Data.K1 * secondOrder.targetVelocity.x) / secondOrder.Data.K2_stable;
        tv.y = secondOrder.targetVelocity.y + deltaTime * (targetRotation.y + secondOrder.Data.K3 * targetVelocity.y - secondOrder.targetPosition.y - secondOrder.Data.K1 * secondOrder.targetVelocity.y) / secondOrder.Data.K2_stable;
        tv.z = secondOrder.targetVelocity.z + deltaTime * (targetRotation.z + secondOrder.Data.K3 * targetVelocity.z - secondOrder.targetPosition.z - secondOrder.Data.K1 * secondOrder.targetVelocity.z) / secondOrder.Data.K2_stable;
        tv.w = secondOrder.targetVelocity.w + deltaTime * (targetRotation.w + secondOrder.Data.K3 * targetVelocity.w - secondOrder.targetPosition.w - secondOrder.Data.K1 * secondOrder.targetVelocity.w) / secondOrder.Data.K2_stable;

        secondOrder.targetVelocity = tv;

        return secondOrder.targetPosition;
    }


}
