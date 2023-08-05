using System;

namespace Test.FSM
{
    public class GenericState<TStateID> : IState<TStateID>
    {
        public TStateID ID { get; }
        
        private Action OnEnter;
        private Action OnUpdate;
        private Action OnExit;
        
        public GenericState(
            TStateID id,
            Action   onEnter,
            Action   onTick = null,
            Action   onExit = null)
        {
            ID      = id;
            OnEnter = onEnter;
            OnUpdate  = onTick;
            OnExit  = onExit;
        }
        
        public void Enter()
        {
            OnEnter?.Invoke();
        }

        public void Update()
        { 
            OnUpdate?.Invoke();
        }

        public void Exit()
        {
            OnExit?.Invoke();
        }
    }
}