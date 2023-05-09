
using UnityEngine;

namespace Test.Architecture
{

    public abstract class ManagerBase : IManager
    {
        private ModulesContainer _modulesContainer;

        public void InitModulesContainer(ModulesContainer modulesContainer)
        {
            _modulesContainer = modulesContainer;
        }

        public abstract void InitDependencyManagers();

        protected T GetManager<T>() where T : IManager
        {
             var t = _modulesContainer.GetManager<T>();
             return t;
        }

        public abstract void Init();

        public abstract void Dispose();
    }
}