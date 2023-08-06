using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Test.AI.States
{
    public class AIPatrolStateBehaviour : BaseAIStateBehaviour
    {
        private AIPatrolStateSettings _settings { get; }
        
        public Vector3 CurPatrolPointPosition { get; private set; }
        
        private Vector3 GetRandomPatrolPoint()
        {
            var randomX = Random.Range(-30, 30);
            var randomZ = Random.Range(-30, 30);
            return new Vector3(randomX, 0, randomZ);
        }


#region Constructor

        public AIPatrolStateBehaviour(AIPatrolStateSettings settings, IAIControllerData data, string id)
            : base(data, id)
        {
            _settings = settings;
        }

#endregion

    }
}