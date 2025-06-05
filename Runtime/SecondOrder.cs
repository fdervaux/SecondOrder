using UnityEngine;

namespace Package.SecondOrder.Runtime
{
    /// <summary>
    /// Class to hold the second order data.
    /// </summary>
    [System.Serializable]
    public class SecondOrder<T>
    {
        /// <summary>
        /// The second order data.
        /// </summary>
        [SerializeField]
        private SecondOrderData _data;

        private bool _isInit;
        private T _lastPosition;
        private T _position, _velocity;

        /// <summary>
        /// The target position.
        /// </summary>
        public T Position { get => _position; set => _position = value; }
    
        /// <summary>
        /// The target velocity.
        /// </summary>
        public T Velocity { get => _velocity; set => _velocity = value; }
    
        /// <summary>
        /// The last position.
        /// </summary>
        public T LastPosition { get => _lastPosition; set => _lastPosition = value; }
    
        /// <summary>
        /// Second order data.
        /// </summary>
        public SecondOrderData Data { get => _data; set => _data = value; }
    
        /// <summary>
        /// Indicates if the second order data is initialized.
        /// </summary>
        public bool IsInit { get => _isInit; set => _isInit = value; }

        /// <summary>
        /// Initializes the second order data with the given position.
        /// </summary>
        /// <param name="position"> The position to initialize with.</param>
        public void Init(T position)
        {
            _lastPosition = position;
            _position = position;
            _isInit = true;
        }

        /// <summary>
        /// Resets the second order data with the given position.
        /// </summary>
        /// <param name="position"> The position to reset with.</param>
        public void Reset(T position)
        {
            _lastPosition = position;
            _position = position;
            _velocity = default;
            _isInit = true;
        }
    }
}
