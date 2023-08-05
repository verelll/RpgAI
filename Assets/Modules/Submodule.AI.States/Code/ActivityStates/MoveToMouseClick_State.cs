using System;
using Test.Game;
using UnityEngine;
using UnityEngine.AI;

namespace Test.AI.States
{
    public class MoveToMouseClick_State : BaseAIState
    {
        public Vector3 TargetPosition { get; private set; }

        private NavMeshAgent agent => DataController.Behaviour.Agent;

        public override void Init()
        {
            //Test
            GameCheats.OnMouseClick += HandleMouseClick;
        }

        public override void Dispose()
        {
            //Test
            GameCheats.OnMouseClick -= HandleMouseClick;
        }

        protected internal override void StartState()
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

        protected internal override void EndState(Action callback = null)
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