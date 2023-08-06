using System;

namespace Test.AI.States
{
    public class AIPatrolStateSettings : BaseAIStateSettings
    {
        public override BaseAIStateBehaviour CreateBehaviour(
            IAIControllerData data)
        {
            return new AIPatrolStateBehaviour(this, data, ID);
        }
    }
}