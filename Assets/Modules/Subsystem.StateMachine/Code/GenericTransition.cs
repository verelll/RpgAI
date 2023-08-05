using System;

namespace Test.FSM
{
    public class GenericTransition<TStateID> : ITransition<TStateID>
    {
        public TStateID   StartState { get; }
        public TStateID   EndState   { get; }
        public Func<bool> Condition  { get; }

        public GenericTransition(TStateID start, TStateID end, Func<bool> cond)
        {
            StartState = start;
            EndState   = end;
            Condition  = cond;
        }

        public bool CanTransit() => Condition();
    }
}