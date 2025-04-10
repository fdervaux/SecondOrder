using UnityEngine;

namespace Package.SecondOrder.Runtime.Example
{
    public class FollowRotationExample : MonoBehaviour
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
        private SecondOrder<Quaternion> _secondOrder;

        private void Start()
        {
            // Initialize the second order filter with the current rotation of the transform. (optional)
            _secondOrder.Init(transform.rotation);
        }

        private void Update()
        {
            // Update the second order filter with the current rotation of the transform.
            transform.rotation = SecondOrderDynamics.Update(_target.rotation,
                _secondOrder, Time.deltaTime);
        }
    }
}