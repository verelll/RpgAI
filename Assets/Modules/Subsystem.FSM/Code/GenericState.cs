using System;

namespace Test.FSM
{
    public class GenericState<I> : IState<I>
    {
        public I ID { get; }
        
        public event Action OnEnter;
        public event Action OnUpdate;
        public event Action OnExit;
        
        public GenericState(
            I id,
            Action   onEnter,
            Action   onUpdate = null,
            Action   onExit = null)
        {
            ID      = id;
            OnEnter = onEnter;
            OnUpdate  = onUpdate;
            OnExit  = onExit;
        }
        
        public virtual void Enter() =>  OnEnter?.Invoke();
        public virtual void Update() => OnUpdate?.Invoke();
        public virtual void Exit() => OnExit?.Invoke();
    }
}