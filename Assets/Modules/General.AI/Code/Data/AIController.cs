using System.Collections.Generic;
using Test.Architecture;
using Test.FSM;
using Test.Stats;
using UnityEngine;

namespace Test.AI
{
    public sealed class AIController : Injector, IAIControllerData
    {
        private StatsManager _statsManager;
        
        public string ID { get; }
        
        public AIModel Model { get; }
        public AIView View { get; }
        public AIPresetConfig Config { get; }

        private FSM<string> _aiFSM;
        private Dictionary<StatType, GenericStatData> _aiStats;

        public AIController(
            string id, 
            AIModel model, 
            AIView view,
            AIPresetConfig config)
        {
            ID = id;
            Model = model;
            View = view;
            Config = config;

            //Set colors
            View.EffectsComponent.Init(Config.LiquidColor, Config.SurfaceColor, Config.FresnelColor);

            ApplyVfx();
            
            //Set params
            var agent = View.Agent;
            agent.speed = Config.moveSpeed;
            agent.angularSpeed = Config.angularSpeed;
            agent.acceleration = Config.acceleration;
            agent.stoppingDistance = Config.stoppingDistance;

            _aiFSM = new FSM<string>();
            _aiStats = new Dictionary<StatType, GenericStatData>();
        }

        public override void InitDependencyManagers()
        {
            _statsManager = MContainer.GetManager<StatsManager>();
        }

        public void Init()
        {
            _aiStats = _statsManager.CreateStatsDataWithView(Config.StatsConfig, View);
            
            foreach (var stateSettings in Config.States)
            {
                var state = stateSettings.CreateBehaviour(this);
                _aiFSM.AddState(state);
            }

            var defaultState = Config.StartState;
            _aiFSM.GoToState(defaultState);

            UnityEventProvider.OnUpdate += Update;
        }
        
        public void Dispose()
        {
            UnityEventProvider.OnUpdate -= Update;
        }

        private void Update()
        {
            _aiFSM?.Update();
        }

        private const string BUBBLE_GRADIENT_NAME = "BubblesGradient";
        
        private void ApplyVfx()
        {
            View.Effect.SetGradient(BUBBLE_GRADIENT_NAME, Config.BubblesGradient);
        }
    }
}