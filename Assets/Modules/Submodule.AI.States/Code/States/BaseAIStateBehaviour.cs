using System;
using System.Collections.Generic;
using Test.FSM;

namespace Test.AI.States
{
    public class BaseAIStateBehaviour : IState<string>
    {
        protected IAIControllerData Data { get; }
        
        public string ID { get; }
        public string NextState { get; }
        public event Action<string> OnComplete;

        public BaseAIStateBehaviour(string id, string nextStateId, IAIControllerData data)
        {
            ID = id;
            NextState = nextStateId;
            Data = data;
        }

        protected void InvokeComplete() => OnComplete?.Invoke(ID);
        
        public virtual void Enter() { }

        public virtual void Update() { }

        public virtual void Exit() { }
        
    }
}