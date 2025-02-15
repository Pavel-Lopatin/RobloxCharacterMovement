using UnityEngine;

namespace Roblox
{
    public class GroundCheck : MonoBehaviour
    {
        public bool IsGrounded => _isGrounded;

        private bool _isGrounded = true;
        private const float ORIGIN_OFFSET = .001f;
        private const float DISTANCE_THRESHOLD = 0.15f;
        private Vector3 _raycastOrigin => transform.position + Vector3.up * ORIGIN_OFFSET;
        private float _raycastDistance => DISTANCE_THRESHOLD + ORIGIN_OFFSET;

        private void LateUpdate()
        {
            bool isGroundedNow = Physics.Raycast(_raycastOrigin, Vector3.down, DISTANCE_THRESHOLD);
            _isGrounded = isGroundedNow;
        }
    }
}