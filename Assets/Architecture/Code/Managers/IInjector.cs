namespace Test.Architecture
{
    public interface IInjector
    {
        public ModulesContainer MContainer { get; }
        internal void InitContainer(ModulesContainer container);
        public void InitDependencyManagers();
    }
}