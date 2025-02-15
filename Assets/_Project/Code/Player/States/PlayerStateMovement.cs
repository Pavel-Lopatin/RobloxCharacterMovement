using Roblox.InputSystem;
using UnityEngine;

namespace Roblox.FSM.Player
{
    public class PlayerStateMovement : FsmState
    {
        protected readonly float _moveSpeed;

        public PlayerStateMovement(Fsm fsm, Rigidbody rigidbody, InputController input, GroundCheck groundCheck, CameraController cameraController, Animator animator, float moveSpeed) : base(fsm, rigidbody, input, groundCheck, cameraController, animator)
        {
            _moveSpeed = moveSpeed;
        }

        public virtual void Move(Vector3 inputDirection, float jumpSpeedCoefficient) 
        {
            Vector3 targetVelocity = inputDirection * _moveSpeed * jumpSpeedCoefficient;

            Vector3 forward = _cameraController.CameraForward;
            Vector3 right = _cameraController.CameraRight;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            forward = forward * targetVelocity.z;
            right = right * targetVelocity.x;

            if (targetVelocity.z != 0 || targetVelocity.x != 0)
            {
                float angle = Mathf.Atan2(forward.x + right.x, forward.z + right.z) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0, angle, 0);
                _rigidbody.transform.rotation = Quaternion.Slerp(_rigidbody.transform.rotation, rotation, 0.15f);
            }

            Vector3 verticalDirection = Vector3.up * _rigidbody.linearVelocity.y;
            Vector3 horizontalDirection = forward + right;

            Vector3 movement = verticalDirection + horizontalDirection;
            _rigidbody.linearVelocity = movement;
        }
        
    }

}