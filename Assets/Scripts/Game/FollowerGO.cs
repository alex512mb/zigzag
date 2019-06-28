using UnityEngine;

namespace Bomberman
{
    /// <summary>
    /// Smoothly follow a target(Transform)  
    /// </summary>
    public class FollowerGO : MonoBehaviour
    {
        [SerializeField] float smoothTime = 0.25f;
        [SerializeField] float maxSpeed = 3;

        [SerializeField] Transform targetFollow;

        Transform m_transform;
        Vector3 currentV;
        Vector3 destinationPos
        {
            get
            {
                return targetFollow.position;
            }
        }

        void Awake()
        {
            m_transform = transform;
        }

        void LateUpdate()
        {
            if (targetFollow == null)
            {
                enabled = false;
                return;
            }

            m_transform.position = Vector3.SmoothDamp(m_transform.position, destinationPos, ref currentV, smoothTime, maxSpeed);
        }
        public void SetTargetFollow(Transform _target)
        {
            targetFollow = _target;
            enabled = true;
        }

    }
}
