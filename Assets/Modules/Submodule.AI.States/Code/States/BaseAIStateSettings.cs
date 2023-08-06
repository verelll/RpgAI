using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Test.AI.States
{
    public abstract class BaseAIStateSettings
    {
        [SerializeField, BoxGroup("Main Settings")]
        private string id;
        public abstract BaseAIStateBehaviour CreateBehaviour(IAIControllerData data);

        public string ID => id;
    }
}