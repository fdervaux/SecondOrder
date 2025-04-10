using Unity.Mathematics;
using UnityEngine;

namespace Package.SecondOrder.Runtime
{
    /// <summary>
    /// Second order data class.
    /// </summary>
    [System.Serializable]
    public class SecondOrderData : ISerializationCallbackReceiver
    {
        /// <summary>
        /// The frequency of the second order system.
        /// </summary>
        [SerializeField, Range(0.001f, 100)] private float _frequency = 1;

        /// <summary>
        /// The damping ratio of the second order system.
        /// </summary>
        [SerializeField, Range(0, 5)] private float _damping = 1;

        /// <summary>
        /// The impulse of the second order system.
        /// </summary>
        [SerializeField, Range(-10, 10)] private float _impulse;

        private float _w, _z, _d, _k1, _k2, _k3;
        private float _k1Stable, _k2Stable;

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
            this._frequency = frequency;
            this._damping = damping;
            this._impulse = impulse;
        }
        
        public float K1Stable { get => _k1Stable; set => _k1Stable = value; }
        public float K2Stable { get => _k2Stable; set => _k2Stable = value; }
        public float K3 { get => _k3; set => _k3 = value; }

        /// <summary>
        /// Update the data of the second order system.
        /// </summary>
        /// <returns></returns>
        public void UpdateData()
        {
            if(_frequency == 0)
                _frequency = 0.001f;

            _w = 2 * Mathf.PI * _frequency;
            _z = _damping;
            _d = _w * Mathf.Sqrt(Mathf.Abs(_damping * _damping - 1));

            _k1 = _damping / (Mathf.PI * _frequency);
            _k2 = 1 / (_w * _w);
            _k3 = _impulse * _damping / _w;
        }

        /// <summary>
        /// Set the delta time for the second order system.
        /// </summary>
        /// <param name="deltaTime"> Delta time for the second order system.</param>
        /// <returns></returns>
        public void SetDeltaTime(float deltaTime)
        {
            if (_w * deltaTime < _z)
            {
                _k1Stable = _k1;
                _k2Stable = Mathf.Max(_k2, deltaTime * deltaTime / 2 + deltaTime * _k1 / 2);
                _k2Stable = Mathf.Max(_k2Stable, deltaTime * _k1);
            }
            else
            {
                float t1 = Mathf.Exp(-_z * _w * deltaTime);
                float alpha = 2 * t1 * (_z <= 1 ? Mathf.Cos(deltaTime * _d) : math.cosh(deltaTime * _d));
                float beta = t1 * t1;
                float t2 = deltaTime / (1 + beta - alpha);

                _k1Stable = (1 - beta) * t2;
                _k2Stable = deltaTime * t2;
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
}
