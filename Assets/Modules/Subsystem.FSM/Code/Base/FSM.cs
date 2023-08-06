using System.Collections.Generic;
using Test.Architecture;

namespace Test.FSM
{
    //I - ID Type
    public sealed class FSM<I> : Injector
    {
        protected Dictionary<I, IState<I>> _states;

        protected Dictionary<I, HashSet<ITransition<I>>> _transitions;

        public I CurrentStateID { get; private set; }
        public IState<I> CurrentState { get; private set; }
        
        public FSM()
        {
            CurrentState = null;
            CurrentStateID = default;
            _states      = new Dictionary<I, IState<I>>();
            _transitions = new Dictionary<I, HashSet<ITransition<I>>>();
        }

#region State
        
        // public void AddState(I id, Action onEnter, Action onUpdate = null, Action onExit = null)
        // {
        //     AddState(new GenericState<I>(id, onEnter, onUpdate, onExit));
        // }
        
        public void AddState<S>(S state) where S: IState<I>
        {
            _states.Add(state.ID, state);
        }
        

#endregion


#region Transition
        
        // public void AddTransition(I from, I to, Func<bool> condition)
        // {
        //     AddTransition(new GenericTransition<I>(from, to, condition));
        // }
        
        public void AddTransition<T>(T transition) where T: ITransition<I>
        {
            if (!_transitions.TryGetValue(transition.StartState, out var transitions))
                transitions = _transitions[transition.StartState] = new HashSet<ITransition<I>>();
            transitions.Add(transition);
        }
        
#endregion

        
#region Logic

        public void GoToState(I id, ITransition<I>transition = null)
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
            
            if (!_transitions.TryGetValue(CurrentStateID, out HashSet<ITransition<I>> transitions))
                return;
            
            foreach (var transit in transitions)
            {
                if (!transit.CanTransit()) 
                    continue;
                
                GoToState(transit.EndState);
                return;
            }
        }

#endregion
//
// #region IInjector
//
//         public ModulesContainer MContainer { get; private set; }
//         void IInjector.InitContainer(ModulesContainer container) => MContainer = container;
//
//         public void InitDependencyManagers() { }
//
// #endregion
    }
}

