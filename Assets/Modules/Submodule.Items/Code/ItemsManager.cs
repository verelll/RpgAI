using Test.Architecture;

namespace Test.Items
{
    public class ItemsManager : ManagerBase
    {
        public override void InitDependencyManagers() { }

        public override void Init()
        {
            ItemConfig.Init();
        }

        public override void Dispose()
        {
            
        }
    }
}
