namespace Test.Architecture
{
    public interface IManager
    {
        public void InitModulesContainer(ModulesContainer modulesContainer);
        public void InitDependencyManagers();

        public void Init();
        public void Dispose();
    }
}