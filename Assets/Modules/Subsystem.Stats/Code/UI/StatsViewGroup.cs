using System.Collections.Generic;
using Test.Architecture;
using UnityEngine;

namespace Test.Stats
{
    public class StatsViewGroup : MonoBehaviour
    {
        [SerializeField]
        private StatView viewPrefab;

        [SerializeField] 
        private Transform container;

        [SerializeField] 
        private RectTransform rectTransform;

        private Dictionary<StatType, GenericStatData> _statsData;
        private List<StatView> _widgets;

        private IStatProvider _provider;
        
        public void Init(Dictionary<StatType, GenericStatData> statsData, IStatProvider provider)
        {
            _widgets = new List<StatView>();
            _statsData = statsData;
            _provider = provider;

            CreateStats();
            UnityEventProvider.OnUpdate += HandleUpdate;
        }

        public void Dispose()
        {
            UnityEventProvider.OnUpdate -= HandleUpdate;
            DestroyStats();
        }

        private void HandleUpdate()
        {
            var screenPos =_provider.ProviderAnchor.ScreenPosition;
            screenPos.z = 0;
     
            
            //rectTransform.localPosition = screenPos;
            rectTransform.position = _provider.ProviderAnchor.UICanvasPosition;
        }

        private void CreateStats()
        {
            foreach (var (type, data) in _statsData)
            {
                var createdView = Instantiate(viewPrefab, container);
                createdView.Init(type, data);
                _widgets.Add(createdView);
            }
        }

        private void DestroyStats()
        {
            foreach (var widget in _widgets)
            {
                widget.Dispose();
                Destroy(widget.gameObject);
            }
            
            _widgets.Clear();
        }
    }
}