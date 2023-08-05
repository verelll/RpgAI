using System;
using System.Collections.Generic;
using Test.Architecture;
using UnityEngine;

namespace Test.AI
{
    public sealed class AIController : Injector, IAIControllerData
    {
        public string ID { get; }
        
        public AIController(
            string id, 
            AIModel model, 
            AIBehaviour behaviour,
            AIPresetConfig config, 
            Material material)
        {
            ID = id;
            Model = model;
            Behaviour = behaviour;
            Config = config;

            //Set material
            var mat = new Material(material)
            {
                color = config.color
            };
            behaviour.SetMaterial(mat);
            
            //Set params
            var agent = Behaviour.Agent;
            agent.speed = Config.moveSpeed;
            agent.angularSpeed = Config.angularSpeed;
            agent.acceleration = Config.acceleration;
            agent.stoppingDistance = Config.stoppingDistance;
            
            // foreach (var con in internalControllers)
            //     AddInternalController(con);
        }

#region IAIControllerData

        public AIModel Model { get; }
        public AIBehaviour Behaviour { get; }
        public AIPresetConfig Config { get; }

#endregion
        

#region Internal Controllers

        private Dictionary<Type, BaseAIInternalController> _internalControllers;
        
        public T GetInternalController<T>() where T : BaseAIInternalController
        {
            var type = typeof(T);
            if (!_internalControllers.TryGetValue(type, out var internalController))
                return default;

            return (T) internalController;
        }

        public void AddInternalController(BaseAIInternalController controller)
        {
            _internalControllers ??= new Dictionary<Type, BaseAIInternalController>();
            var type = controller?.GetType();
            
            if(_internalControllers.ContainsKey(type))
               return;

            MContainer.Inject(controller);
            controller?.InitController();
            _internalControllers[type] = controller;
        }

        public void RemoveInternalController(BaseAIInternalController controller)
        {
            var type = controller?.GetType();
            
            if(!_internalControllers.ContainsKey(type))
               return;

            var con = _internalControllers[type];
            con.DisposeController();
            _internalControllers.Remove(type);
        }
        
#endregion

    }
}