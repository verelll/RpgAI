using System;
using System.Collections.Generic;

namespace Test.Architecture
{
    public class ModulesContainer 
    {
        private Dictionary<Type, ManagerBase> _managers = new ();
        
        public void InitManagers()
        {
            foreach (var pair in _managers)
            {
                Inject(pair.Value);
                pair.Value.Init();
            }
        }

        public void DisposeManagers()
        {
            foreach (var pair in _managers)
            {
                pair.Value.Dispose();
            }
        }

        public T AddManager<T>() where T : ManagerBase, new()
        {
            var newModule = new T();
            var type = newModule.GetType();
            _managers.Add(type, newModule);
            return newModule;
        }

        public T GetManager<T>() where T : ManagerBase
        {
            var type = typeof(T);
            if (!_managers.TryGetValue(type, out var module))
                return default;

            return (T) module;
        }
        
        public void Inject(IInjector injector)
        {
            injector.InitContainer(this);
            injector.InitDependencyManagers();
        }
    }
}