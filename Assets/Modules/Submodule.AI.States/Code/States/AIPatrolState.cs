using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Test.AI.States
{
    public class AIPatrolState<I> : BaseAIState<I>
    {
        public Vector3 CurPatrolPointPosition { get; private set; }

        private IEnumerator _moveRoutine;
        
        public override void Enter()
        {

            base.Enter();
        }
        
        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
        
        private Vector3 GetRandomPatrolPoint()
        {
            var randomX = Random.Range(-30, 30);
            var randomZ = Random.Range(-30, 30);
            return new Vector3(randomX, 0, randomZ);
        }


        #region Constructor

        public AIPatrolState(IAIControllerData data, I id, Action onEnter, Action onUpdate = null, Action onExit = null) 
            : base(data, id, onEnter, onUpdate, onExit) { }

        #endregion

    }
}