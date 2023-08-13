using UnityEngine;
using Random = UnityEngine.Random;

namespace Test.AI.States
{
    public class AIPatrolStateBehaviour : BaseAIStateBehaviour
    {
        private AIPatrolStateSettings _settings;

        private Vector3 _curPatrolPoint;
        private int _curRepeatsCount;
        
        public override void Enter()
        {
            ApplyNextPoint();
        }

        public override void Update()
        {
            if(_curPatrolPoint == Vector3.zero)
                return;
            
            if (TryComplete())
                return;

            var agent = Data.View.Agent;
            if (agent.remainingDistance <= _settings.FinishDistance)
            {
                _curRepeatsCount++;
                ApplyNextPoint();
                return;
            }
        }

        public override void Exit()
        {
            _curRepeatsCount = 0;
        }

        private void ApplyNextPoint()
        {
            _curPatrolPoint = GetRandomPatrolPoint();
            Data.View.Agent.SetDestination(_curPatrolPoint);
        }
        
        private bool TryComplete()
        {
            if (_curRepeatsCount < _settings.RepeatPatrolsCount) 
                return false;
            
            InvokeComplete();
            return true;
        }
        
        private Vector3 GetRandomPatrolPoint()
        {
            var randomX = Random.Range(-30, 30);
            var randomZ = Random.Range(-30, 30);
            return new Vector3(randomX, 0, randomZ);
        }

        public AIPatrolStateBehaviour(AIPatrolStateSettings settings, string id, string nextStateId, IAIControllerData data) 
            : base(id, nextStateId, data)
        {
            _settings = settings;
        }
    }
}