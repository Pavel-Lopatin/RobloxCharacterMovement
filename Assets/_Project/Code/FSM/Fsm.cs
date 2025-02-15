using System;
using System.Collections.Generic;

namespace Roblox.FSM
{
    public class Fsm
    {
        private Dictionary<Type, FsmState> _states = new Dictionary<Type, FsmState>();

        private FsmState CurrentState { get; set; }
         
        public void AddState(FsmState state) => _states.Add(state.GetType(), state);

        public void SetState<T>() where T : FsmState
        {
            var type = typeof(T);

            if (CurrentState != null && CurrentState.GetType() == type)
                return;

            if (_states.TryGetValue(type, out var newState))
            {
                CurrentState?.Exit();
                CurrentState = newState;
                CurrentState.Enter();
            }
        }

        public void Update() => CurrentState?.Update();
        public void FixedUpdate() => CurrentState?.FixedUpdate();
    }
}