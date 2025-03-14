﻿using Roblox.InputSystem;
using UnityEngine;

namespace Roblox.FSM
{
    public abstract class FsmState
    {
        protected readonly Fsm _fsm;
        protected readonly InputController _input;
        protected readonly Rigidbody _rigidbody;
        protected readonly GroundCheck _groundCheck;
        protected readonly CameraController _cameraController;
        protected readonly Animator _animator;
        protected readonly PlayerConfig _playerConfig;

        public FsmState(Fsm fsm, Rigidbody rigidbody,  InputController input, GroundCheck groundCheck, CameraController cameraController, Animator animator, PlayerConfig playerConfig)
        {
            _fsm = fsm;
            _input = input;
            _rigidbody = rigidbody;
            _groundCheck = groundCheck;
            _cameraController = cameraController;
            _animator = animator;
            _playerConfig = playerConfig;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}