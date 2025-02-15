using Roblox.InputSystem;
using UnityEngine;

namespace Roblox.FSM.Player
{
    public class PlayerStateRun : PlayerStateMovement
    {
        private float _moveSpeed;

        public PlayerStateRun(Fsm fsm, Rigidbody rigidbody, InputController input, GroundCheck groundCheck, CameraController cameraController, float moveSpeed) : base(fsm, rigidbody, input, groundCheck, cameraController)
        {
            _moveSpeed = moveSpeed;
        }

        public override void Enter()
        {
            Debug.Log($"Movement ({GetType().Name}) state [ENTER]");
            _input.OnSpaceButtonClicked += JumpCheck;
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

        public override void Move(Vector3 inputDirection, Vector3 cameraForward)
        {
            Vector3 targetVelocity = inputDirection * _moveSpeed;

            _rigidbody.transform.forward = cameraForward;
            //_rigidbody.transform.forward += inputDirection;
            _rigidbody.linearVelocity = _rigidbody.rotation * new Vector3(targetVelocity.x, _rigidbody.linearVelocity.y, targetVelocity.z);


        }

        private void JumpCheck()
        {
            Debug.Log($"Movement ({GetType().Name}) checked [JUMP]");
            if (_groundCheck.IsGrounded)
                _fsm.SetState<PlayerStateJump>();
        }
    }
}