using Test.Architecture;
using UnityEngine;

namespace Test.Stats
{
    [CreateAssetMenu(
        fileName = "StatsSettings",
        menuName = "Stats/Settings",
        order = 10)]
    public sealed class StatsSettings : SingletonScriptableObject<StatsSettings>
    {
        [SerializeField]
        private StatsViewGroup viewGroupPrefab;
        
        public StatsViewGroup GroupPrefab => viewGroupPrefab;
    }
}