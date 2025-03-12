using Roblox.InputSystem;
using UnityEngine;

namespace Roblox.FSM.Player
{
    public class PlayerStateJump : PlayerStateMovement
    {
        public PlayerStateJump(Fsm fsm, Rigidbody rigidbody, InputController input, GroundCheck groundCheck, CameraController cameraController, Animator animator, PlayerConfig playerConfig) : base(fsm, rigidbody, input, groundCheck, cameraController, animator, playerConfig)
        {
        }

        public override void Enter()
        {
            Debug.Log($"Movement ({GetType().Name}) state [ENTER]");
            _animator.SetTrigger("Jump");
            Jump();
        }

        public override void Exit()
        {
            Debug.Log($"Movement ({GetType().Name}) state [EXIT]");
            _animator.ResetTrigger("Jump");
        }

        public override void Update()
        {
            Debug.Log($"Movement ({GetType().Name}) state [UPDATE]");

            if (_groundCheck.IsGrounded && _input.MoveDirection == Vector3.zero)
                _fsm.SetState<PlayerStateIdle>();
            if (_groundCheck.IsGrounded && _input.MoveDirection != Vector3.zero)
                _fsm.SetState<PlayerStateRun>();
        }

        public override void FixedUpdate()
        {
            Move(_input.MoveDirection, _playerConfig.JumpMoveCoefficient);
        }

        private void Jump()
        {
            _groundCheck.DisableGroundCheck();
            _rigidbody.AddForce(Vector3.up * _playerConfig.JumpForce, ForceMode.Impulse);
        }
    }
}