using Test.Architecture;

namespace Test.FSM
{
    public abstract class Condition : Injector
    {
        public string ID { get; }
        public abstract bool CheckCondition();

        public Condition(string fsmId)
        {
            ID = fsmId;
        }
    }
}