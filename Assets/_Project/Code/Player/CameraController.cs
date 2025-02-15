using Roblox.InputSystem;
using UnityEngine;

namespace Roblox
{
    public class CameraController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _camera;
        [SerializeField] private InputController _inputController;

        [Header("Parameters")]
        [SerializeField] private float _targetdistance = 5.0f;
        [SerializeField] private float _xSpeed = 120.0f;
        [SerializeField] private float _ySpeed = 120.0f;
        [SerializeField] private float _scrollSensitivity = 4f;
        [SerializeField] private float _smoothnessX = 0.1f;
        [SerializeField] private float _smoothnessY = 1f;
        [SerializeField] private float _smoothnessZoom = 1f;

        [Header("Camera limits")]
        [SerializeField] private float _distanceMin = 0.5f;
        [SerializeField] private float _distanceMax = 15f;
        [SerializeField] private float _yMinLimit = -20f;
        [SerializeField] private float _yMaxLimit = 80f;

        public Vector3 CameraForward => Vector3.ProjectOnPlane(_camera.forward, transform.up);
        public Vector3 CameraRight => Vector3.ProjectOnPlane(_camera.right, transform.up);
        public bool IsMouseHoldNow => _isMouseHold;

        private float x, y, _targetX, _targetY, _distance;
        private bool _isMouseHold = false;

        private void Awake()
        {
            _inputController.OnMouseRightButtonClicked += IsMouseHold;
            _inputController.OnMouseRightButtonReleased += IsMouseHold;

            if (!_camera)
                _camera = Camera.main.transform;
        }

        private void Start()
        {
            Vector3 angles = _camera.transform.eulerAngles;
            x = angles.y;
            y = angles.x;
        }

        private void LateUpdate()
        {
            RotateCamera(_inputController.MouseAxis, _inputController.MouseScroll);
        }

        public void IsMouseHold() => _isMouseHold = !_isMouseHold;

        private void RotateCamera(Vector2 mouseAxis, float mouseScroll)
        {
            if (_isMouseHold)
            {
                _targetX += mouseAxis.x * _xSpeed * 0.02f;
                _targetY -= mouseAxis.y * _ySpeed * 0.02f;
            }
            _targetY = ClampAngle(_targetY, _yMinLimit, _yMaxLimit);

            x = Mathf.LerpAngle(x, _targetX, _smoothnessX);
            y = Mathf.LerpAngle(y, _targetY, _smoothnessY);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            _targetdistance = Mathf.Clamp(_targetdistance - (mouseScroll * _scrollSensitivity), _distanceMin, _distanceMax);
            _distance = Mathf.Lerp(_distance, _targetdistance, _smoothnessZoom);
            Vector3 newDistance = new Vector3(0.0f, 0.0f, -_distance);
            Vector3 position = rotation * newDistance + _target.position;

            _camera.transform.rotation = rotation;
            _camera.transform.position = position;
        }

        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F) angle += 360F;
            if (angle > 360F) angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }

        private void OnDestroy()
        {
            _inputController.OnMouseRightButtonClicked -= IsMouseHold;
            _inputController.OnMouseRightButtonReleased -= IsMouseHold;
        }
    }
}


