using System;
using System.Collections.Generic;
using Test.Architecture;

namespace Test.FSM
{
    public class FSM<TStateID> : IInjector
    {
        private Dictionary<TStateID, IState<TStateID>> _states;

        private Dictionary<TStateID, HashSet<ITransition<TStateID>>> _transitions;

        public TStateID CurrentStateID { get; private set; }
        public IState<TStateID> CurrentState { get; private set; }
        
        public FSM()
        {
            CurrentState = null;
            CurrentStateID = default;
            _states      = new Dictionary<TStateID, IState<TStateID>>();
            _transitions = new Dictionary<TStateID, HashSet<ITransition<TStateID>>>();
        }

        public void AddState(IState<TStateID> state)
        {
            _states.Add(state.ID, state);
        }

        public void AddTransition(ITransition<TStateID> transition)
        {
            if (!_transitions.TryGetValue(transition.StartState, out var transitions))
                transitions = _transitions[transition.StartState] = new HashSet<ITransition<TStateID>>();
            transitions.Add(transition);
        }

        public void AddState(TStateID id, Action onEnter, Action onUpdate = null, Action onExit = null)
        {
            AddState(new GenericState<TStateID>(id, onEnter, onUpdate, onExit));
        }
        
        public void AddTransition(TStateID from, TStateID to, Func<bool> condition)
        {
            AddTransition(new GenericTransition<TStateID>(from, to, condition));
        }

        public void GoToState(TStateID id, ITransition<TStateID>transition = null)
        {
            transition?.OnBeforeLeave();
            CurrentState?.Exit();
            
            CurrentStateID = default;
            CurrentState = null;
            transition?.OnTransit();
            
            CurrentStateID = id;
            if (!_states.TryGetValue(CurrentStateID, out var newState)) 
                return;
            
            CurrentState = newState;
            CurrentState.Enter();
            transition?.OnAfterEnter();
        }
        
        public void Update()
        {
            CurrentState?.Update();
            
            if (!_transitions.TryGetValue(CurrentStateID, out HashSet<ITransition<TStateID>> transitions))
                return;
            
            foreach (var transit in transitions)
            {
                if (transit.CanTransit())
                {
                    GoToState(transit.EndState);
                    return;
                }
            }
        }


#region IInjector

        public ModulesContainer MContainer { get; private set; }
        void IInjector.InitContainer(ModulesContainer container) => MContainer = container;

        public void InitDependencyManagers() { }

#endregion
    }
}

