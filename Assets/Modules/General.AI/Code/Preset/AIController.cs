using UnityEngine;

namespace Test.AI
{
    public class AIController
    {
        private AIModel _model { get; }
        private AIBehaviour _behaviour { get; }
        
        private AIPresetConfig _settings;
        
        public AIController(AIModel model, AIBehaviour behaviour, AIPresetConfig config)
        {
            _model = model;
            _behaviour = behaviour;
            _settings = config;

            //Set material
            behaviour.SetMaterial(config.mainMaterial);
            
            //Set params
            var agent = _behaviour.Agent;
            agent.speed = _settings.moveSpeed;
            agent.angularSpeed = _settings.angularSpeed;
            agent.acceleration = _settings.acceleration;
            agent.stoppingDistance = _settings.stoppingDistance;
        }
        

        public void MoveTo(Vector3 targetPosition)
        {
            var agent = _behaviour.Agent;
            agent.SetDestination(targetPosition);
        }
    }
}