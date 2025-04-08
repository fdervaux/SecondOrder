using UnityEngine;
using Unity.Mathematics;


/// <summary>
/// Second order data class.
/// </summary>
[System.Serializable]
public class SecondOrderData : ISerializationCallbackReceiver
{
    /// <summary>
    /// The frequency of the second order system.
    /// </summary>
    [SerializeField, Range(0, 100)] private float frequency = 1;

    /// <summary>
    /// The damping ratio of the second order system.
    /// </summary>
    [SerializeField, Range(0, 5)] private float damping = 1;

    /// <summary>
    /// The impulse of the second order system.
    /// </summary>
    [SerializeField, Range(-10, 10)] private float impulse = 0;

    private float _w, _z, _d, _k1, _k2, _k3;
    private float k1_stable, k2_stable;

    /// <summary>
    /// Constructor for the second order data class.
    /// </summary>
    public SecondOrderData()
    {
    }

    /// <summary>
    /// Constructor for the second order data class.
    /// </summary>
    /// <param name="frequency"> Frequency of the second order system.</param>
    /// <param name="damping"> Damping ratio of the second order system.</param>
    /// <param name="impulse"> Impulse of the second order system.</param>
    public SecondOrderData(float frequency, float damping, float impulse)
    {
        this.frequency = frequency;
        this.damping = damping;
        this.impulse = impulse;
    }

    public float K1 { get => _k1; set => _k1 = value; }
    public float K2_stable { get => k2_stable; set => k2_stable = value; }
    public float K3 { get => _k3; set => _k3 = value; }

    /// <summary>
    /// Update the data of the second order system.
    /// </summary>
    /// <returns></returns>
    public void UpdateData()
    {
        _w = 2 * Mathf.PI * frequency;
        _z = damping;
        _d = _w * Mathf.Sqrt(Mathf.Abs(damping * damping - 1));

        _k1 = damping / (Mathf.PI * frequency);
        _k2 = 1 / (_w * _w);
        _k3 = impulse * damping / _w;
    }

    /// <summary>
    /// Set the delta time for the second order system.
    /// </summary>
    /// <param name="deltaTime"> Delta time for the second order system.</param>
    /// <returns></returns>
    public void setDeltaTime(float deltaTime)
    {
        if (_w * deltaTime < _z)
        {
            k1_stable = _k1;
            k2_stable = Mathf.Max(_k2, deltaTime * deltaTime / 2 + deltaTime * _k1 / 2);
            k2_stable = Mathf.Max(k2_stable, deltaTime * _k1);
        }
        else
        {
            float t1 = Mathf.Exp(-_z * _w * deltaTime);
            float alpha = 2 * t1 * (_z <= 1 ? Mathf.Cos(deltaTime * _d) : math.cosh(deltaTime * _d));
            float beta = t1 * t1;
            float t2 = deltaTime / (1 + beta - alpha);

            k1_stable = (1 - beta) * t2;
            k2_stable = deltaTime * t2;
        }
    }

    /// <summary>
    /// Callback function before the serialization.
    /// </summary>
    /// <returns></returns>
    public void OnBeforeSerialize() { }

    /// <summary>
    /// Callback function after the deserialization.
    /// </summary>
    /// <returns></returns>
    public void OnAfterDeserialize()
    {
        UpdateData();
    }
}
