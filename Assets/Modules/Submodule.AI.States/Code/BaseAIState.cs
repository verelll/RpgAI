using System;
using System.Collections;
using Test.Architecture;
using UnityEngine;

namespace Test.AI.States
{
    public abstract class BaseAIState : Injector
    {
        public IAIControllerData DataController { get; private set; }
        internal void InitAIController(IAIControllerData controller) => DataController = controller;

        public event Action OnStateStart;
        public event Action OnStateUpdate;
        public event Action OnStateEnd;
        
        private void InvokeStartState() => OnStateStart?.Invoke();
        private void InvokeUpdateState() => OnStateUpdate?.Invoke();
        private void InvokeEndState() => OnStateEnd?.Invoke();


        public virtual void Init() { }

        public virtual void Dispose() { }

        protected internal virtual void StartState()
        {
            InvokeStartState();
            UnityEventProvider.OnUpdate += UpdateState;
        }

        protected virtual void UpdateState()
        {
            InvokeUpdateState();
        }

        protected internal virtual void EndState(Action callback = null)
        {
            UnityEventProvider.OnUpdate -= UpdateState;
            
            InvokeEndState();
            callback?.Invoke();
        }

#region Move To

        private IEnumerator _activeMovement;

        protected void MoveToPosition(Vector3 targetPosition, float finishDistance, Action callback = null)
        {
            if (_activeMovement != null)
            {
                UnityEventProvider.CoroutineStop(_activeMovement);
                _activeMovement = null;
            }
            
            _activeMovement = MoveToPositionCoroutine(targetPosition, finishDistance, callback);
            UnityEventProvider.CoroutineStart(_activeMovement);
        }
        
        private IEnumerator MoveToPositionCoroutine(Vector3 targetPosition, float finishDistance, Action callback = null)
        {
            var agent = DataController.Behaviour.Agent;
            agent.SetDestination(targetPosition);
           
            while (agent.remainingDistance > finishDistance)
                yield return null;

            _activeMovement = null;
            callback?.Invoke();
        }

#endregion

public BaseAIState Id { get; }
    }
}