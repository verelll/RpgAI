using System;
using Test.FSM;

namespace Test.AI.States
{
    public class BaseAIStateBehaviour : GenericState<string>
    {
        protected IAIControllerData Data { get; }

        public BaseAIStateBehaviour(IAIControllerData data, string id) 
            : base(id)
        {
            Data = data;
        }
    }
}