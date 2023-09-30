using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Test.Stats
{
    public class StatView : MonoBehaviour
    {
        [SerializeField] 
        private Image _fillSliderImage;
        
        [SerializeField] 
        private Image _backgroundSliderImage;
        
        [SerializeField] 
        private Slider _valueSlider;

        private StatType _type;
        private GenericStatData _data;
        
        public void Init(StatType type, GenericStatData data)
        {
            _data = data;
            _type = type;

            UpdateView();
            _data.OnValueChanged += HandleValueChanged;
            _data.OnMaxValueChanged += HandleMaxValueChanged;
        }

        public void Dispose()
        {
            _data.OnValueChanged -= HandleValueChanged;
            _data.OnMaxValueChanged -= HandleMaxValueChanged;
        }

        private void UpdateView()
        {
            _fillSliderImage.color = _data.DataSettings.FillColor;
            _backgroundSliderImage.color = _data.DataSettings.BackFillColor;

            _valueSlider.minValue = 0;
            _valueSlider.maxValue = _data.MaxAmount;

            _valueSlider.value = _data.Amount;
        }
        
        private void HandleValueChanged(float fromValue, float toValue)
        {
            _valueSlider.value = _data.Amount;
        }
        
        private void HandleMaxValueChanged(float fromValue, float toValue)
        {
            _valueSlider.maxValue = _data.MaxAmount;
            HandleValueChanged(0, 0);
        }
    }
}