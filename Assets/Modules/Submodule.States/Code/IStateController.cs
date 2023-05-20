using System;
using System.Collections.Generic;

namespace Test.States
{
    public interface IStateController
    {
        public Dictionary<Type, BaseAIState> AllStates { get; }
        
        public BaseAIState ActiveState { get;}
        
        T AddState<T>(bool initState) where T : BaseAIState, new();

        void RemoveState<T>() where T : BaseAIState;

        void RemoveAllStates();

        T GetState<T>() where T : BaseAIState;

        void ActivateState<T>(Action callback = null) where T : BaseAIState;
    }
}