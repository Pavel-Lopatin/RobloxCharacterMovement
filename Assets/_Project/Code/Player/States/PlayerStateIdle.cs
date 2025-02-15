using Roblox.InputSystem;
using UnityEngine;

namespace Roblox.FSM.Player
{
    public class PlayerStateIdle : FsmState
    {
        public PlayerStateIdle(Fsm fsm, Rigidbody rigidbody, InputController input, GroundCheck groundCheck, CameraController cameraController, Animator animator) : base(fsm, rigidbody, input, groundCheck, cameraController, animator)
        {
        }

        public override void Enter()
        {
            Debug.Log($"Movement ({GetType().Name}) state [ENTER]");
            _input.OnSpaceButtonClicked += JumpCheck;
            _animator.SetTrigger("Idle");
        }

        public override void Exit()
        {
            Debug.Log($"Movement ({GetType().Name}) state [EXIT]");
            _input.OnSpaceButtonClicked -= JumpCheck;
            _animator.ResetTrigger("Idle");
        }

        public override void Update()
        {
            Debug.Log($"Movement ({GetType().Name}) state [UPDATE]");

            if (_input.MoveDirection != Vector3.zero)
                _fsm.SetState<PlayerStateRun>();

            _rigidbody.linearVelocity = new Vector3(0f, _rigidbody.linearVelocity.y, 0f);
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


