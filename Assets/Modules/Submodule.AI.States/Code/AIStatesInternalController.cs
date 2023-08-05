using Test.Architecture;
using Test.FSM;

namespace Test.AI.States
{
    public enum FSMStateType
    {
        Idle = 0,
        Walk = 1
    }
    
    public sealed class AIStatesInternalController : BaseAIInternalController
    {
        private FSM<FSMStateType> _curFsm;

        private int a;
        
        public override void InitController()
        {
            // _curFsm = new FSM<FSMStateType>();
            // _curFsm.AddState(FSMStateType.Idle, HandleOnEnter);
            // _curFsm.AddTransition(FSMStateType.Idle, FSMStateType.Walk, () => a > 0);

            UnityEventProvider.OnUpdate += _curFsm.Update;
        }

        public override void DisposeController()
        {
            UnityEventProvider.OnUpdate -= _curFsm.Update;
        }
        
        internal AIStatesInternalController(IAIControllerData controllerData) : base(controllerData) { }
    }
}