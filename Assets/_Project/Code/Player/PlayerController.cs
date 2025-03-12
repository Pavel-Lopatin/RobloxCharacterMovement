using Roblox.FSM;
using Roblox.FSM.Player;
using Roblox.InputSystem;
using UnityEngine;

namespace Roblox
{
    [RequireComponent(typeof(InputController), typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private InputController _input;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private Animator _animator;

        [Header("Config")]
        [SerializeField] private PlayerConfig _playerConfig;

        private Fsm _fsm;

        public InputController Input => _input;
        public Rigidbody Rigidbody => _rigidbody;
        public GroundCheck GroundCheck => _groundCheck;
        public CameraController CameraController => _cameraController;
        public Animator Animator => _animator;

        private void Start()
        {
            _fsm = new Fsm();
            _fsm.AddState(new PlayerStateIdle(_fsm, Rigidbody, Input, GroundCheck, CameraController, Animator, _playerConfig));
            _fsm.AddState(new PlayerStateRun(_fsm, Rigidbody, Input, GroundCheck, CameraController, Animator, _playerConfig));
            _fsm.AddState(new PlayerStateJump(_fsm, Rigidbody, Input, GroundCheck, CameraController, Animator, _playerConfig));

            _fsm.SetState<PlayerStateIdle>();
        }

        private void Update()
        {
            _fsm.Update();
        }

        private void FixedUpdate()
        {
            _fsm.FixedUpdate();
        }
    }
}