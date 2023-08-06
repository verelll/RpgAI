namespace Test.FSM
{
    public interface IState<I>
    {
        I ID { get; }
        
        public  void Enter();
        public  void Update();
        public  void Exit();
    }
}