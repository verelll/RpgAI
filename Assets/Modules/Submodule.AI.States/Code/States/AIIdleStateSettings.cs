using Sirenix.OdinInspector;
using UnityEngine;

namespace Test.AI.States
{
    public class AIIdleStateSettings : BaseAIStateSettings
    {
        [PropertySpace(10)]
        [SerializeField, BoxGroup("Settings/Idle Settings"), MinMaxSlider(0, 15)]
        private Vector2Int waitTime = new Vector2Int(3, 5);

        public int WaitTime => Random.Range(waitTime.x, waitTime.y);
        
        public override BaseAIStateBehaviour CreateBehaviour(IAIControllerData data)
        {
            return new AIIdleStateBehaviour(this, ID, NextStateID, data);
        }
    }
}