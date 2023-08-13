using System.Collections.Generic;
using Test.Architecture;
using UnityEngine;

namespace Test.FSM
{
    //I - ID Type
    public sealed class FSM<I> : Injector
    {
        protected Dictionary<I, IState<I>> _states;
        
        public I CurrentStateID { get; private set; }
        public IState<I> CurrentState { get; private set; }
        
        public FSM()
        {
            CurrentState = null;
            CurrentStateID = default;
            _states      = new Dictionary<I, IState<I>>();
        }
        
        public void AddState(IState<I> state) 
        {
            _states.Add(state.ID, state);
        }

        public void GoToState(I id)
        {
            CurrentState?.Exit();
            
            CurrentStateID = default;
            CurrentState = null;
            
            CurrentStateID = id;
            if (!_states.TryGetValue(CurrentStateID, out var newState))
            {
                Debug.Log($"[FSM] State not found: {id}");
                return;
            }
            
            CurrentState = newState;
            CurrentState.OnComplete += HandleStateComplete;
            CurrentState.Enter();
        }

        private void HandleStateComplete(I state)
        {
            CurrentState.OnComplete -= HandleStateComplete;
            
            GoToState(CurrentState.NextState);
        }
        
        public void Update()
        {
            CurrentState?.Update();
        }
    }
}

