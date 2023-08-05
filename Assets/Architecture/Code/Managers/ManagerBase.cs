namespace Test.Architecture
{
    public abstract class ManagerBase : Injector
    {
        public virtual void Init(){}
        public virtual void Dispose(){}
        
        protected T GetManager<T>() where T : ManagerBase
        {
             return  MContainer.GetManager<T>();
        }
    }
}