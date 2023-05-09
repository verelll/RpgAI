using System.Collections.Generic;
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
        public Material mainMaterial;
        
        [Header("Move Settings")]
        public float moveSpeed = 3.5f;
        public float angularSpeed = 120;
        public float acceleration = 8;
        public float stoppingDistance = 0;

        [Header("Group Settings")] 
        public string groupId;
        public List<string> friendlyGroups;
        public List<string> enemyGroups;

        [Header("Attack Settings")] 
        public bool canAttack;

        public string ID => name;
    }
}