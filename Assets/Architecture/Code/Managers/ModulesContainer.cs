using System;
using System.Collections.Generic;

namespace Test.Architecture
{
    public class ModulesContainer
    {
        private Dictionary<Type, IManager> _managers;

        public void InitContainer()
        {
            _managers = new Dictionary<Type, IManager>();
        }

        public void InitManagers()
        {
            foreach (var pair in _managers)
            {
                pair.Value.InitModulesContainer(this);
                pair.Value.InitDependencyManagers();
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

        public T AddManager<T>() where T : IManager, new()
        {
            var newModule = new T();
            var type = newModule.GetType();
            _managers.Add(type, newModule);
            return newModule;
        }

        public T GetManager<T>() where T : IManager
        {
            var type = typeof(T);
            if (!_managers.TryGetValue(type, out var module))
                return default;

            return (T) module;
        }
    }
}