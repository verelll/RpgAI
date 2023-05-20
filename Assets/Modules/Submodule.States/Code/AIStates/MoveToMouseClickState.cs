using System;
using Test.AI;
using Test.Game;
using Test.States;
using UnityEngine;
using UnityEngine.AI;

namespace Test.AI.States
{
    public class MoveToMouseClickState : BaseAIState
    {
        public Vector3 TargetPosition { get; private set; }

        private NavMeshAgent agent => controller.Behaviour.Agent;

        public override void Init(AIController controller)
        {
            base.Init(controller);
            
            //Test
            GameCheats.OnMouseClick += HandleMouseClick;
        }

        public override void Dispose()
        {
            GameCheats.OnMouseClick -= HandleMouseClick;
            
            base.Dispose();
        }

        public override void StartState()
        {
            if (TargetPosition == null)
            {
                Debug.Log("[MoveAIState] Target is null!");
                return;
            }
               
            agent.SetDestination(TargetPosition);
            base.StartState();
        }

        protected override void UpdateState()
        {
            base.UpdateState();
            if(agent.remainingDistance <= 2)
                EndState();
        }

        public override void EndState(Action callback = null)
        {
            TargetPosition = Vector3.zero;
            base.EndState(callback);
        }

        private void HandleMouseClick(Vector3 pos)
        {
            TargetPosition = pos;
            StartState();
        }
    }
}