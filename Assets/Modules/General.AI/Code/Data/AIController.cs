using Test.Architecture;
using UnityEngine;

namespace Test.AI
{
    public sealed class AIController : Injector, IAIControllerData
    {
        public string ID { get; }
        
        public AIModel Model { get; }
        public AIView View { get; }
        public AIPresetConfig Config { get; }

        public AIController(
            string id, 
            AIModel model, 
            AIView view,
            AIPresetConfig config, 
            Material material)
        {
            ID = id;
            Model = model;
            View = view;
            Config = config;

            //Set material
            var mat = new Material(material)
            {
                color = config.color
            };
            view.SetMaterial(mat);
            
            //Set params
            var agent = View.Agent;
            agent.speed = Config.moveSpeed;
            agent.angularSpeed = Config.angularSpeed;
            agent.acceleration = Config.acceleration;
            agent.stoppingDistance = Config.stoppingDistance;
        }

        public void Init()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}