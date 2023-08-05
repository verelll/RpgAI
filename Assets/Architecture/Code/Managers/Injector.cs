namespace Test.Architecture
{
    public abstract class Injector : IInjector
    {
        public ModulesContainer MContainer { get; private set; }
        void IInjector.InitContainer(ModulesContainer container) => MContainer = container;
        public virtual void InitDependencyManagers() { }
    }
}