using Test.Architecture;
using UnityEngine;

namespace Test.AI.Triggers
{
    public abstract class BaseAITrigger
    {
        
    }
    
    public class AITriggerInternalController : BaseAIInternalController
    {
        public override void InitController()
        {
            
        }

        public override void DisposeController()
        {
            

        }


        internal AITriggerInternalController(IAIControllerData controllerData) : base(controllerData) { }
    }
}

