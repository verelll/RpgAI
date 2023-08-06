using System;

namespace Test.FSM
{
    public class GenericTransition<I> : ITransition<I>
    {
        public I   StartState { get; }
        public I   EndState   { get; }

        public GenericTransition(I start, I end)
        {
            StartState = start;
            EndState   = end;
        }

        public virtual bool CanTransit() => false;
    }
}