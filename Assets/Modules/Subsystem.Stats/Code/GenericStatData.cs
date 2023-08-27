namespace Test.Stats
{
    public enum StatType
    {
        None = 0,
        
        Health = 100,
        Armor = 200,
        Energy = 300,
        
        Food = 500,
        Water = 600,
    }
    
    public class GenericStatData : BaseStatData<StatType>
    {
        public GenericStatData(StatType id,float defaultValue, float defaultMaxValue, StatDataSettings dataSettings) 
            : base(id, defaultValue, defaultMaxValue, dataSettings) { }
    }
}