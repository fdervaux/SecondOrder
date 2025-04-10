using UnityEngine;

namespace Package.SecondOrder.Runtime.Example
{
    /// <summary>
    /// FollowTargetExample is a simple example of how to use the FollowTarget component.
    /// </summary>
    public class FollowTargetExample : MonoBehaviour
    {
        /// <summary>
        /// Target is the transform that you want to follow.
        /// </summary>
        [SerializeField]
        private Transform _target;
        
        /// <summary>
        /// SecondOrder is a component that allows you to follow a target with a second order filter.
        /// </summary>
        [SerializeField]
        private SecondOrder<Vector3> _secondOrder;

        private void Start()
        {
            // Initialize the second order filter with the current position of the transform. (optional)
            _secondOrder.Init(transform.position);
        }

        private void Update()
        {
            // Update the second order filter with the current position of the transform.
            transform.position = SecondOrderDynamics.Update(_target.position,
                _secondOrder, Time.deltaTime);
        }
    }
}
