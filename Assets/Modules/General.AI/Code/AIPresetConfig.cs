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
        [Header("Main Settings")] 
        public Color color;
        
        [Header("Move Settings")]
        public float moveSpeed = 3.5f;
        public float angularSpeed = 120;
        public float acceleration = 8;
        public float stoppingDistance = 0;
        
        
        public string ID => name;
    }
}