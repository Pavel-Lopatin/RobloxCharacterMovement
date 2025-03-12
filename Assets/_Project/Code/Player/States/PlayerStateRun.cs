using Roblox.InputSystem;
using UnityEngine;

namespace Roblox.FSM.Player
{
    public class PlayerStateRun : PlayerStateMovement
    {
        public PlayerStateRun(Fsm fsm, Rigidbody rigidbody, InputController input, GroundCheck groundCheck, CameraController cameraController, Animator animator, PlayerConfig playerConfig) : base(fsm, rigidbody, input, groundCheck, cameraController, animator, playerConfig)
        {
        }

        public override void Enter()
        {
            Debug.Log($"Movement ({GetType().Name}) state [ENTER]");
            _input.OnSpaceButtonClicked += JumpCheck;
            _animator.SetTrigger("Run");
        }

        public override void Exit()
        {
            Debug.Log($"Movement ({GetType().Name}) state [EXIT]");
            _input.OnSpaceButtonClicked -= JumpCheck;
            _animator.ResetTrigger("Run");

        }

        public override void Update()
        {
            Debug.Log($"Movement ({GetType().Name}) state [UPDATE]");

            if (_groundCheck.IsGrounded && _input.MoveDirection == Vector3.zero)
                _fsm.SetState<PlayerStateIdle>();
        }

        public override void FixedUpdate()
        {
            Move(_input.MoveDirection, 1f);
        }

        private void JumpCheck()
        {
            if (_groundCheck.IsGrounded)
            {
                _fsm.SetState<PlayerStateJump>();
                Debug.Log($"Movement ({GetType().Name}) checked [JUMP] succesfully");
            }
        }
    }
}