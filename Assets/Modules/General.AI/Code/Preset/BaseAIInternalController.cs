using Test.Architecture;

namespace Test.AI
{
    public class BaseAIInternalController : Injector
    {
        protected IAIControllerData ControllerData { get; private set; }
        internal BaseAIInternalController(IAIControllerData controllerData) { ControllerData = controllerData; }
        
        public virtual void InitController() { }
        public virtual void DisposeController() { }
    }
}