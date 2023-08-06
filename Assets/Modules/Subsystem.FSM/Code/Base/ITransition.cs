namespace Test.FSM
{
    public interface ITransition<I>
    {
        I StartState { get; }
        I EndState   { get; }

        bool CanTransit();

        void OnBeforeLeave() { }
        void OnTransit() { }
        void OnAfterEnter() { }
    }
}