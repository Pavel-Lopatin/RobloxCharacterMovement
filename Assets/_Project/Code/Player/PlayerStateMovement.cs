using Roblox.InputSystem;
using UnityEngine;

namespace Roblox.FSM.Player
{
    public class PlayerStateMovement : FsmState
    {
        public PlayerStateMovement(Fsm fsm, Rigidbody rigidbody, InputController input, GroundCheck groundCheck, CameraController cameraController) : base(fsm, rigidbody, input, groundCheck, cameraController)
        {
        }

        public override void Enter()
        {
            Debug.Log($"Movement ({GetType().Name}) state [ENTER]");
            _input.OnSpaceButtonClicked += JumpCheck;
        }

        public override void Exit()
        {
            Debug.Log($"Movement ({GetType().Name}) state [EXIT]");
            _input.OnSpaceButtonClicked -= JumpCheck;
        }

        public override void Update()
        {
            Debug.Log($"Movement ({GetType().Name}) state [UPDATE]");

            if (_input.MoveDirection == Vector3.zero)
                _fsm.SetState<PlayerStateIdle>();

        }

        private void JumpCheck()
        {
            Debug.Log($"Movement ({GetType().Name}) checked [JUMP], {_groundCheck.IsGrounded}");

            if (_groundCheck.IsGrounded)
                _fsm.SetState<PlayerStateJump>();
        }

        public virtual void Move(Vector3 inputDirection, Vector3 cameraForward) { }
        
    }

}