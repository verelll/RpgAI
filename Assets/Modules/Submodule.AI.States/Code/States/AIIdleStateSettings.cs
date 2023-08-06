using System;

namespace Test.AI.States
{
    public class AIIdleStateSettings : BaseAIStateSettings
    {
        public override BaseAIStateBehaviour CreateBehaviour(IAIControllerData data)
        {
            return new AIIdleStateBehaviour(this, data, ID);
        }
    }
}