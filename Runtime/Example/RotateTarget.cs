using UnityEngine;

namespace Package.SecondOrder.Runtime.Example
{
    /// <summary>
    /// Class to rotate a target randomly within a sphere.
    /// </summary>
    public class RotateTarget : MonoBehaviour
    {
        /// <summary>
        /// Rotation speed of the target.
        /// </summary>
        [SerializeField] private float _rotationSpeed = 10f;
        
        /// <summary>
        /// Pause time before rotating to a new target.
        /// </summary>
        [SerializeField] private float _pauseTime = 1f;

        private Vector3 _targetRotation;
        private float _pauseTimeRemaining;

        private void Start()
        {
            _targetRotation = Random.insideUnitSphere * 360f;
        }
        
        private void Update()
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(_targetRotation),
                Time.deltaTime * _rotationSpeed);


            // Check if the object has reached the target rotation
            if (!(Quaternion.Angle(transform.rotation, Quaternion.Euler(_targetRotation)) < 0.1f)) return;
        
            // Pause for a moment
            if (_pauseTimeRemaining > 0)
            {
                _pauseTimeRemaining -= Time.deltaTime;
                return; // Wait until the pause time is over
            }

            // Reset the pause time
            _pauseTimeRemaining = _pauseTime;

            _targetRotation = Random.insideUnitSphere * 360f;
        }
    }
}