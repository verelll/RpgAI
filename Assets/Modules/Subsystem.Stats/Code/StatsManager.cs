using System.Collections.Generic;
using Test.Architecture;
using Test.UI;
using UnityEngine;

namespace Test.Stats
{
    public sealed class StatsManager : ManagerBase
    {
        private UIManager _uiManager;

        private StatsSettings _settings;
        
        public override void InitDependencyManagers()
        {
            _uiManager = GetManager<UIManager>();
            _settings = StatsSettings.Instance;
        }

#region Logic

        public Dictionary<StatType, GenericStatData> CreateStatsDataWithView(StatsPresetConfig presetConfig, IStatProvider provider)
        {
            var stats = CreateStatsData(presetConfig);
            CreateStatsView(stats, provider);
            return stats;
        }

        public Dictionary<StatType, GenericStatData> CreateStatsData(StatsPresetConfig presetConfig)
        {
            var stats = new Dictionary<StatType, GenericStatData>();
            foreach (var (statType, setting) in presetConfig.Stats)
            {
                if(stats.ContainsKey(statType))
                    continue;

                var createdData = CreateStatData(statType, setting);
                stats[statType] = createdData;
            }

            return stats;
        }

        private GenericStatData CreateStatData(StatType type, StatDataSettings dataSettings)
        {
            var createdStat = new GenericStatData(type, dataSettings.CurAmount, dataSettings.MaxAmount, dataSettings);
            return createdStat;
        }

        public void DestroyStats(IStatProvider provider)
        {
            DestroyStatsView(provider);
        }
        
#endregion


#region UI View

        private Dictionary<IStatProvider, StatsViewGroup> _groupWidgets = new Dictionary<IStatProvider, StatsViewGroup>();

        private void CreateStatsView(Dictionary<StatType, GenericStatData> statsData, IStatProvider provider)
        {
            var createdGroupWidget = GameObject.Instantiate(_settings.GroupPrefab, _uiManager.Hierarchy.StatsLayer);
            createdGroupWidget.Init(statsData, provider);
            _groupWidgets.Add(provider, createdGroupWidget);
        }
        
        private void DestroyStatsView(IStatProvider provider)
        {
            if(!_groupWidgets.TryGetValue(provider, out var widget))
                return;
                
            widget.Dispose();
            _groupWidgets.Remove(provider);
            GameObject.Destroy(widget.gameObject);
        }

#endregion
    }
}