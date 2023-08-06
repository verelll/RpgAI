using System;
using Test.FSM;

namespace Test.AI.States
{
    public class BaseAITransition<T> : GenericTransition<T>
    {
        protected IAIControllerData Data { get; }
        
        public override bool CanTransit() => false;

#region Constructor

        public BaseAITransition(IAIControllerData data,T start, T end) 
            : base(start, end)
        {
            Data = data;
        }
        
#endregion

    }
}