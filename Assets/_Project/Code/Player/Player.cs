using Roblox.FSM;
using Roblox.FSM.Player;
using Roblox.InputSystem;
using UnityEngine;

namespace Roblox
{
    [RequireComponent(typeof(InputController), typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private InputController _input;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private CameraController _cameraController;

        [Header("Parameters")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField, Range(0.1f, 1f)] private float _jumpMoveCoefficient;

        private Fsm _fsm;

        public InputController Input => _input;
        public Rigidbody Rigidbody => _rigidbody;
        public GroundCheck GroundCheck => _groundCheck;
        public CameraController CameraController => _cameraController;
        public float MoveSpeed => _moveSpeed;
        public float JumpForce => _jumpForce;
        public float JumpMoveCoefficient => _jumpMoveCoefficient;

        private void Start()
        {
            _fsm = new Fsm();
            _fsm.AddState(new PlayerStateIdle(_fsm, Rigidbody, Input, GroundCheck, CameraController));
            _fsm.AddState(new PlayerStateRun(_fsm, Rigidbody, Input, GroundCheck, CameraController, MoveSpeed));
            _fsm.AddState(new PlayerStateJump(_fsm, Rigidbody, Input, GroundCheck, CameraController, MoveSpeed, JumpForce, JumpMoveCoefficient));

            _fsm.SetState<PlayerStateIdle>();
        }

        private void Update()
        {
            _fsm.Update();
            _fsm.FixedUpdate();
        }
    }
}

