using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Test.AI.States
{
    public class Patrol_State : BaseAIState
    {
        public Vector3 CurPatrolPointPosition { get; private set; }
        
        public override void Init()
        {
            // StartState();
        }

        protected internal override void StartState()
        {
            base.StartState();
            CurPatrolPointPosition = GetRandomPatrolPoint();
            MoveToPosition(CurPatrolPointPosition, 2, () => EndState());
        }

        protected internal override void EndState(Action callback = null)
        {
            base.EndState(callback);
            // StartState();
        }

        private Vector3 GetRandomPatrolPoint()
        {
            var randomX = Random.Range(-30, 30);
            var randomZ = Random.Range(-30, 30);
            return new Vector3(randomX, 0, randomZ);
        }
    }
}