using System;

namespace Test.FSM
{
    public class GenericState<I> : IState<I>
    {
        public I ID { get; private set; }
        
        public event Action OnEnter;
        public event Action OnUpdate;
        public event Action OnExit;

        public GenericState(I id)
        {
            ID = id;
        }

        public virtual void Enter() =>  OnEnter?.Invoke();
        public virtual void Update() => OnUpdate?.Invoke();
        public virtual void Exit() => OnExit?.Invoke();
    }
}