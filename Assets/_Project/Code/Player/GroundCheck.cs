using System.Collections;
using UnityEngine;

namespace Roblox
{
    public class GroundCheck : MonoBehaviour
    {
        public bool IsGrounded => _isGrounded;

        private bool _isGrounded = true;
        private bool _canCheck = true;
        private const float DISABLE_TIME = 0.1f;
        private const float ORIGIN_OFFSET = .001f;
        private const float DISTANCE_THRESHOLD = 0.05f;
        private Vector3 _raycastOrigin => transform.position + Vector3.up * ORIGIN_OFFSET;
        private float _raycastDistance => DISTANCE_THRESHOLD + ORIGIN_OFFSET;
        private Coroutine _disableCoroutine;

        private void Update()
        {
            if (_canCheck)
                _isGrounded = Physics.Raycast(_raycastOrigin, Vector3.down, DISTANCE_THRESHOLD);
        }

        public void DisableGroundCheck()
        {
            StartCoroutine(DisableGroundCheckCoroutine());
        }

        private IEnumerator DisableGroundCheckCoroutine()
        {
            _isGrounded = false;
            _canCheck = false;
            yield return new WaitForSeconds(DISABLE_TIME);
            _canCheck = true;
        }
    }
}