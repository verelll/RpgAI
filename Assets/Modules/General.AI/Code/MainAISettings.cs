using System.Collections.Generic;
using Test.Architecture;
using UnityEngine;

namespace Test.AI
{
    [CreateAssetMenu(
        fileName = "MainAISettings", 
        menuName = "AI/MainAISettings", 
        order = 10)]
    public class MainAISettings : SingletonScriptableObject<MainAISettings>
    {
        public AIView aiPrefab;

        public List<AIPresetConfig> presetSpawnList;
    }
}