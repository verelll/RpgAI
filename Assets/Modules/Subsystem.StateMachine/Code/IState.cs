namespace Test.FSM
{
    public interface IState<TStateID>
    {
        TStateID ID { get; }
        
        public  void Enter();
        public  void Update();
        public  void Exit();
    }
}