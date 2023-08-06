namespace Test.AI
{
    public interface IAIControllerData
    {
        public AIModel Model { get; }
        public AIView View { get; }
        public AIPresetConfig Config { get; }
    }
}