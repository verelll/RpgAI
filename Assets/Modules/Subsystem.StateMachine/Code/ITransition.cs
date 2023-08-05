namespace Test.FSM
{
    public interface ITransition<TStateID>
    {
        TStateID StartState { get; }
        TStateID EndState   { get; }

        bool CanTransit();

        void OnBeforeLeave() { }
        void OnTransit() { }
        void OnAfterEnter() { }
    }
}