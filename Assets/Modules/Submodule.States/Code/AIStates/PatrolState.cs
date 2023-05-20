using System;
using Test.AI;
using Test.States;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Test.AI.States
{
    public class PatrolState : BaseAIState
    {
        public Vector3 CurPatrolPointPosition { get; private set; }
        
        private NavMeshAgent agent => controller.Behaviour.Agent;

        public override void Init(AIController controller)
        {
            base.Init(controller);
            StartState();
        }

        public override void StartState()
        {
            base.StartState();
            CurPatrolPointPosition = GetRandomPatrolPoint();
            agent.SetDestination(CurPatrolPointPosition);
        }

        protected override void UpdateState()
        {
            base.UpdateState();
            if(agent.remainingDistance <= 2)
                EndState();
        }

        public override void EndState(Action callback = null)
        {
            base.EndState(callback);
            StartState();
        }

        private Vector3 GetRandomPatrolPoint()
        {
            var randomX = Random.Range(-30, 30);
            var randomZ = Random.Range(-30, 30);
            return new Vector3(randomX, 0, randomZ);
        }
    }
}