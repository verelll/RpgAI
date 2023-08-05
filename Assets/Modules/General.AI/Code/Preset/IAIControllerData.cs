namespace Test.AI
{
    public interface IAIControllerData
    {
        public AIModel Model { get; }
        public AIBehaviour Behaviour { get; }
        public AIPresetConfig Config { get; }
    }
}