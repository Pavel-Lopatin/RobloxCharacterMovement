using System;
using UnityEngine;

namespace Roblox.InputSystem
{
    public class InputController : MonoBehaviour
    {
        public event Action OnMouseRightButtonClicked;
        public event Action OnMouseRightButtonReleased;
        public event Action OnSpaceButtonClicked;

        public Vector3 MoveDirection => _moveDirection;
        public Vector2 MouseAxis => _mouseAxis;
        public float MouseScroll => _mouseScroll;

        private Vector3 _moveDirection;
        private Vector2 _mouseAxis;
        private float _mouseScroll;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) OnMouseRightButtonClicked?.Invoke();
            if (Input.GetMouseButtonUp(0)) OnMouseRightButtonReleased?.Invoke();
            if (Input.GetKeyDown(KeyCode.Space)) OnSpaceButtonClicked?.Invoke();

            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
            _mouseAxis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")).normalized;
            _mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        }
    }
}