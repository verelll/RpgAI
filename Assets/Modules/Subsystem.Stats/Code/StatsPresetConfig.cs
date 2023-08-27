using System.Collections.Generic;
using Sirenix.OdinInspector;
using Test.Architecture;
using UnityEngine;

namespace Test.Stats
{
    [CreateAssetMenu(
        fileName = "StatsPresetConfig",
        menuName = "Stats/PresetConfig",
        order = 10)]
    public sealed class StatsPresetConfig : MultitonScriptableObjectsByName<StatsPresetConfig>
    {
        [SerializeField]
        private Dictionary<StatType, StatDataSettings> stats;

        public IReadOnlyDictionary<StatType, StatDataSettings> Stats => stats;

        public StatDataSettings GetStatByType(StatType statType)
        {
            if (!stats.TryGetValue(statType, out var stat))
                return default;

            return stat;
        }
    }

    public sealed class StatDataSettings
    {
        [SerializeField, HorizontalGroup, LabelText("MAX")]
        private float maxAmount;

        [SerializeField, HorizontalGroup, LabelText("CUR")]
        private float curAmount;

        [SerializeField, BoxGroup] 
        private Sprite icon;

        [SerializeField, BoxGroup] 
        private Color fillColor;
        
        [SerializeField, BoxGroup] 
        private Color backFillColor;

        public float MaxAmount => maxAmount;
        public float CurAmount => curAmount;

        public Sprite Icon => icon;

        public Color FillColor => fillColor;
        public Color BackFillColor => backFillColor;
    }
}
