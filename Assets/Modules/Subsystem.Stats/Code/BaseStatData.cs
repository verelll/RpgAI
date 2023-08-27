using System;

namespace Test.Stats
{
    public abstract class BaseStatData<I>
    {
        public I Id { get; }
        public StatDataSettings DataSettings { get; }
        
        private float _maxAmount;
        private float _amount;

        public float MaxAmount
        {
            get => _maxAmount;
            set
            {
                var prevValue = _maxAmount;
                _maxAmount = value;
                OnMaxValueChanged?.Invoke(prevValue, _maxAmount);
            }
        }
        
        public float Amount
        {
            get => _amount;
            set
            {
                var prevAmount = _amount;
                if (_amount + value > _maxAmount)
                    _amount = _maxAmount;
                else
                    _amount += value;
                
                OnValueChanged?.Invoke(prevAmount, _amount);
            }
        }
        
        //First - from, Second - to;
        public event Action< float, float> OnMaxValueChanged;
        public event Action<float, float> OnValueChanged;
        
        public BaseStatData(I id, float defaultValue, float defaultMaxValue, StatDataSettings dataSettings)
        {
            Id = id;
            _amount = defaultValue;
            _maxAmount = defaultMaxValue;
            DataSettings = dataSettings;
        }
    }
}