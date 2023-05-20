namespace Test.Items
{
    public class ItemInstance
    {
        public ItemConfig Config { get;}
        public ItemBehaviour Behaviour { get; private set; }
        
        public ItemInstance(ItemConfig config)
        {
            Config = config;
        }

        public void SetBehaviour(ItemBehaviour behaviour)
        {
            Behaviour = behaviour;
        }

        public void Init()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}