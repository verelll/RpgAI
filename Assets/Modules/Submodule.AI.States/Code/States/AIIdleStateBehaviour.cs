using System;
using System.Collections;
using System.Collections.Generic;
using Test.Architecture;
using UnityEngine;

namespace Test.AI.States
{
    public class AIIdleStateBehaviour : BaseAIStateBehaviour
    {
        private AIIdleStateSettings _settings;

        private IEnumerator _waitRoutine;

        public override void Enter()
        {
            _waitRoutine = AwaitTime(_settings.WaitTime, InvokeComplete);
            UnityEventProvider.CoroutineStart(_waitRoutine);
        }

        public override void Exit()
        {
            if(_waitRoutine != null)
                UnityEventProvider.CoroutineStop(_waitRoutine);
        }

        private IEnumerator AwaitTime(float waitTime, Action callback = null)
        {
            yield return new WaitForSeconds(waitTime);
            callback?.Invoke();
        }
        
        public AIIdleStateBehaviour(AIIdleStateSettings settings, string id, string nextStateId, IAIControllerData data) 
            : base(id, nextStateId, data)
        {
            _settings = settings;
        }
    }
}