namespace Test.Architecture
{
    public class GlobalModulesContainer : SingletonMonoBehaviour<GlobalModulesContainer>
    {
        private ModulesContainer _modulesContainer;

        public void SetContainer(ModulesContainer container)
        {
            _modulesContainer = container;
        }
        
        public T GetManager<T>() where T : ManagerBase
        {
            return _modulesContainer.GetManager<T>();
        }
    }
}