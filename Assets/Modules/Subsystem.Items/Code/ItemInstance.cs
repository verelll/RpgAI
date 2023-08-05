namespace Test.Items
{
    public class ItemInstance
    {
        public string ID { get; }
        public ItemConfig Config { get;}
        public IItemView View { get; private set; }
        public ItemEquippedView EquippedView => View is ItemEquippedView equippedView ? equippedView : null;
        public ItemView BasicView => View is ItemView basicView ? basicView : null;
        
        public ItemInstance(ItemConfig config, string id)
        {
            Config = config;
            ID = id;
        }

        public void SetView(IItemView view) { View = view;}
        
        public void Init() { }

        public void Dispose() { }
    }
}