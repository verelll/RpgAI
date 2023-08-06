using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Test.Architecture
{
    public static class ExtensionAI
    {
        public static IEnumerator MoveToPositionCoroutine(this NavMeshAgent agent, Vector3 targetPosition, float finishDistance, Action callback = null)
        {
            agent.SetDestination(targetPosition);
           
            while (agent.remainingDistance > finishDistance)
                yield return null;

            callback?.Invoke();
        }
    }
}
