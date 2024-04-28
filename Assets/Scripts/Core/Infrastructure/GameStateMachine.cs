using System;
using System.Collections.Generic;

namespace Core.Infrastructure
{
    public class GameStateMachine 
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;
        
        public GameStateMachine(Dictionary<Type, IState> states)
        {
            _states = new()
            {
                [typeof(BootstrapState)] = new BootstrapState(this),
            };
        }

        public void Enter<TState>() where TState : IState
        {
            _activeState?.Exit();
            var state = _states[typeof(TState)];
            _activeState = state;
            state.Enter();
        }
    }
}