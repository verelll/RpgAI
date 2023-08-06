using System.Collections.Generic;
using Sirenix.OdinInspector;
using Test.AI.States;
using Test.Architecture;
using UnityEngine;

namespace Test.AI
{
    [CreateAssetMenu(
        fileName = "AIPresetConfig",
        menuName = "AI/AIPresetConfig",
        order = 10)]
    public class AIPresetConfig : MultitonScriptableObjectsByName<AIPresetConfig>
    {
        [BoxGroup("Main Settings")] public Color color;
        
        [BoxGroup("Move Settings")] public float moveSpeed = 3.5f;
        [BoxGroup("Move Settings")] public float angularSpeed = 120;
        [BoxGroup("Move Settings")] public float acceleration = 8;
        [BoxGroup("Move Settings")] public float stoppingDistance = 0;

        [BoxGroup("States Settings"), SerializeField] private List<BaseAIStateSettings> states = new();

        public string ID => name;
        public IEnumerable<BaseAIStateSettings> States => states;
    }
}