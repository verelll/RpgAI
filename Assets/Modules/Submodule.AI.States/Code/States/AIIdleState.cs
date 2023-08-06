using System;

namespace Test.AI.States
{
    public class AIIdleState<I> : BaseAIState<I>
    {
        public AIIdleState(IAIControllerData data, I id, Action onEnter, Action onUpdate = null, Action onExit = null) 
            : base(data, id, onEnter, onUpdate, onExit) { }
    }
}