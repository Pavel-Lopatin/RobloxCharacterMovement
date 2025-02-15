using Roblox.InputSystem;
using UnityEngine;

namespace Roblox.FSM.Player
{
    public class PlayerStateJump : PlayerStateMovement
    {
        private float _moveSpeed;
        private float _jumpForce;
        private float _jumpMoveCoefficent;

        public PlayerStateJump(Fsm fsm, Rigidbody rigidbody, InputController input, GroundCheck groundCheck, CameraController cameraController, float moveSpeed, float jumpForce, float jumpMoveCoefficient) : base(fsm, rigidbody, input, groundCheck, cameraController)
        {
            _moveSpeed = moveSpeed;
            _jumpForce = jumpForce;
            _jumpMoveCoefficent = jumpMoveCoefficient;
        }

        public override void Enter()
        {
            Debug.Log($"Movement ({GetType().Name}) state [ENTER]");
            Jump();
        }

        public override void Exit()
        {
            Debug.Log($"Movement ({GetType().Name}) state [EXIT]");
        }

        public override void Update()
        {
            if (_groundCheck.IsGrounded && _input.MoveDirection == Vector3.zero)
                _fsm.SetState<PlayerStateIdle>();
            else if (_groundCheck.IsGrounded && _input.MoveDirection != Vector3.zero)
                _fsm.SetState<PlayerStateRun>();
        }

        public override void FixedUpdate()
        {
            Move(_input.MoveDirection, _cameraController.CameraForward);
        }

        private void Jump()
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        public override void Move(Vector3 inputDirection, Vector3 cameraForward)
        {
            Vector3 targetVelocity = inputDirection * _moveSpeed * _jumpMoveCoefficent;

            _rigidbody.linearVelocity = cameraForward + new Vector3(targetVelocity.x, _rigidbody.linearVelocity.y * -1000f, targetVelocity.z);

            _rigidbody.transform.forward = targetVelocity;
        }

    }
}