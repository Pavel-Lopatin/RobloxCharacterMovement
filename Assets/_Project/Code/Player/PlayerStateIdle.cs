using Roblox.InputSystem;
using UnityEngine;

namespace Roblox.FSM.Player
{
    public class PlayerStateIdle : FsmState
    {
        public PlayerStateIdle(Fsm fsm, Rigidbody rigidbody, InputController input, GroundCheck groundCheck, CameraController cameraController) : base(fsm, rigidbody, input, groundCheck, cameraController)
        {
        }

        public override void Enter()
        {
            Debug.Log("Idle state [ENTER]");
            _input.OnSpaceButtonClicked += JumpCheck;
        }

        public override void Exit()
        {
            Debug.Log("Idle state [EXIT]");
            _input.OnSpaceButtonClicked -= JumpCheck;
        }

        public override void Update()
        {
            Debug.Log("Idle state [UPDATE]");

            if (_input.MoveDirection != Vector3.zero)
                _fsm.SetState<PlayerStateRun>();

            _rigidbody.linearVelocity = Vector3.zero;
        }

        private void JumpCheck()
        {
            Debug.Log($"Movement ({GetType().Name}) checked [JUMP]");
            if (_groundCheck.IsGrounded)
                _fsm.SetState<PlayerStateJump>();
        }
    }
}


