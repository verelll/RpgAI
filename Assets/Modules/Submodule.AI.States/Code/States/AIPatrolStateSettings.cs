using Sirenix.OdinInspector;
using UnityEngine;

namespace Test.AI.States
{
    public class AIPatrolStateSettings : BaseAIStateSettings
    {
        [SerializeField, BoxGroup("Settings/Patrol Settings")] private int repeatPatrolsCount;
        [SerializeField, BoxGroup("Settings/Patrol Settings")] private float finishDistance;

        public int RepeatPatrolsCount => repeatPatrolsCount;
        public float FinishDistance => finishDistance;

        public override BaseAIStateBehaviour CreateBehaviour(IAIControllerData data)
        {
            return new AIPatrolStateBehaviour(this, ID, NextStateID, data);
        }
    }
}