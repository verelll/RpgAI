using System;
using Test.AI;
using Test.Architecture;

namespace Test.States
{
    public abstract class BaseAIState
    {
        protected AIController controller { get; private set; }

        public event Action OnStateStart;
        public event Action OnStateUpdate;
        public event Action  OnStateEnd;
        
        private void InvokeStartState() => OnStateStart?.Invoke();
        private void InvokeUpdateState() => OnStateUpdate?.Invoke();
        private void InvokeEndState() => OnStateEnd?.Invoke();

        public virtual void Init(AIController controller)
        {
            this.controller = controller;
        }

        public virtual void Dispose()
        {
            
        }

        public virtual void StartState()
        {
            InvokeStartState();
            UnityEventProvider.OnUpdate += UpdateState;
        }

        protected virtual void UpdateState()
        {
            InvokeUpdateState();
        }

        public virtual void EndState(Action callback = null)
        {
            UnityEventProvider.OnUpdate -= UpdateState;
            
            InvokeEndState();
            callback?.Invoke();
        }
    }
}