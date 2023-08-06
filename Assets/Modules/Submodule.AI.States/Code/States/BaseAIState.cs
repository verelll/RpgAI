using System;
using Test.FSM;

namespace Test.AI.States
{
    public class BaseAIState<T> : GenericState<T>
    {
        protected IAIControllerData Data { get; }

#region Constructor

        public BaseAIState(IAIControllerData data, T id, Action onEnter, Action onUpdate = null, Action onExit = null) :
            base(id, onEnter, onUpdate, onExit)
        {
            Data = data;
        }

#endregion

    }
}