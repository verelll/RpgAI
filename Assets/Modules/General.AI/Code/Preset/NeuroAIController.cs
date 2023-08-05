using Test.AI.States;

namespace Test.AI.Neuro
{
    public class NeuroAIController
    {
        private AIController _aiController;
        
        public void Start(AIController controller)
        {
            _aiController = controller;
            
            //Activity states
            // _aiController.AddState<Patrol_ActivityState>();
            // _aiController.AddState<MoveToMouseClick_ActivityState>();
            //
            // //Passive states
            // _aiController.AddState<Look_PassiveState>();
            //
            // _aiController.InitStates();
            //
            // _aiController.AddStateToStack<Patrol_ActivityState>();
            // _aiController.StartStatesStack();
            //
            // void AddRecursivenessState()
            // {
            //     _aiController.AddStateToStack<Patrol_ActivityState>(false, AddRecursivenessState);
            // }

            
        }

        public void Dispose()
        {
            
        }
    }
}