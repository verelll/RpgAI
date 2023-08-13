using System;

namespace Test.FSM
{
    public interface IState<I>
    {
        I ID { get; }
        
        I NextState { get; }
        
        public event Action<I> OnComplete;
        
        public  void Enter();
        public  void Update();
        public  void Exit();
        
    }
}