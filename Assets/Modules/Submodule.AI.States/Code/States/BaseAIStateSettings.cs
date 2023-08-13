using Sirenix.OdinInspector;
using UnityEngine;

namespace Test.AI.States
{
    public abstract class BaseAIStateSettings
    {
        [SerializeField, BoxGroup("Settings", false), LabelText("Current State")]
        private string stateId;
        
        [SerializeField, BoxGroup("Settings", false), LabelText("Next State")]
        private string nextStateId;

        public abstract BaseAIStateBehaviour CreateBehaviour(IAIControllerData data);

        public string ID => stateId;
        
        public string NextStateID => 
            string.IsNullOrEmpty(nextStateId) 
            ? ID 
            : nextStateId;
    }
}