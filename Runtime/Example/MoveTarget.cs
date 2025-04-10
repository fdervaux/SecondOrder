using UnityEngine;

namespace Package.SecondOrder.Runtime.Example
{
    /// <summary>
    /// Class to move a target randomly within a circle.
    /// </summary>
    public class MoveTarget : MonoBehaviour
    {
        /// <summary>
        /// Speed of the target.
        /// </summary>
        [SerializeField] private float _speed = 10f;
        
        /// <summary>
        /// Pause time before moving to a new target.
        /// </summary>
        [SerializeField] private float _pauseTime = 1f;
        
        /// <summary>
        /// Circle radius within which the target can move.
        /// </summary>
        [SerializeField] private float _circleRadius = 0.75f;

        private Vector3 _targetPosition;
        private float _pauseTimeRemaining;

        private void Start()
        {
            _targetPosition = Random.insideUnitCircle * _circleRadius;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Time.deltaTime * _speed);

            // Check if the object has reached the target position
            if (!(Vector3.Distance(transform.position, _targetPosition) < 0.1f)) return;

            // Pause for a moment
            if (_pauseTimeRemaining > 0)
            {
                _pauseTimeRemaining -= Time.deltaTime;
                return; // Wait until the pause time is over
            }

            // Reset the pause time
            _pauseTimeRemaining = _pauseTime;

            _targetPosition = Random.insideUnitCircle * _circleRadius;
            while (Vector3.Distance(transform.position, _targetPosition) < _circleRadius * 0.5f - Mathf.Epsilon)
            {
                _targetPosition = Random.insideUnitCircle * _circleRadius;
            }
        }
    }
}