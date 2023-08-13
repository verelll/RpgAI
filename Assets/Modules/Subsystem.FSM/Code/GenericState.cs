using System;
using System.Collections.Generic;

namespace Test.FSM
{
    public class GenericState<I> : IState<I>
    {
        public I ID { get;}
        public I NextState { get; }
        public event Action<I> OnComplete;

        public GenericState(I id)
        {
            ID = id;
        }

        public virtual void Enter(){}
        public virtual void Update(){}
        public virtual void Exit(){}
        public virtual bool CanExit() => true;
    }
}