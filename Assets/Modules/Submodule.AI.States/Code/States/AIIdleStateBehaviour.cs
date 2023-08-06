using System;

namespace Test.AI.States
{
    public class AIIdleStateBehaviour : BaseAIStateBehaviour
    {
        private AIIdleStateSettings _settings { get; }
        
        
        public AIIdleStateBehaviour(AIIdleStateSettings settings, IAIControllerData data, string id)
            : base(data, id)
        {
            _settings = settings;
        }
    }
}