using System.Collections.Generic;
using Test.Architecture;
using UnityEngine;

namespace Test.AI
{
    public class AIManager : ManagerBase
    {
        public  IReadOnlyDictionary<string, AIController> AIControllers => _aiList;
        private Dictionary<string, AIController> _aiList;

        private AIHierarchy _aiHierarchy;
        private MainAISettings _settings;
        
        public override void Init()
        {
            AIPresetConfig.Init();

            _aiList = new Dictionary<string, AIController>();
            
            _aiHierarchy = GameObject.FindObjectOfType<AIHierarchy>();
            _settings = MainAISettings.Instance;
        }

        public AIController GetByID(string id)
        {
            if (!_aiList.TryGetValue(id, out var value))
                return default;

            return value;
        }

        public AIController SpawnRandomAI(Vector3 position)
        {
            var randomIndex = Random.Range(0, _settings.presetSpawnList.Count);
            var randomPreset = _settings.presetSpawnList[randomIndex];
            return CreateAI(randomPreset, position);
        }

        public AIController CreateAI(AIPresetConfig presetConfig)
        {
            return CreateAIController(presetConfig, Vector3.zero, Quaternion.identity);
        }
        
        public AIController CreateAI(AIPresetConfig presetConfig, Vector3 position)
        {
            return CreateAIController(presetConfig, position, Quaternion.identity);
        }

#region Controller

        private AIController CreateAIController(AIPresetConfig presetConfig, Vector3 position, Quaternion rotation)
        {
            var curPos = position;
            var curRot = rotation;
            
            if(position == Vector3.zero)
            {
                var spawnPoint = GetRandomSpawnPoint();
                curPos = spawnPoint.position;
                curRot = spawnPoint.localRotation;
            }
            
            var model = CreateAIModel();
            var view = CreateAIView(
                _settings.aiPrefab,
                curPos,
                curRot);

            var id = GenerateAIName(presetConfig.ID);
            view.gameObject.name = id;

            var controller = new AIController(id, model, view, presetConfig, _settings.aiMaterial);
            MContainer.Inject(controller);
            _aiList.Add(id, controller);
            controller.Init();
            return controller;
        }
        
        public void DestroyAIController(AIController controller)
        {
            if(controller == null)
                return;
            
            if(!_aiList.ContainsKey(controller.ID))
                return;
            
            DestroyAIView(controller.View, 1);
            _aiList[controller.ID] = null;
            controller.Dispose();
        }

#endregion
        

#region Model

        private AIModel CreateAIModel()
        {
            return new AIModel();
        }
        

#endregion


#region View

        private AIView CreateAIView(AIView prefab, Vector3 position, Quaternion rotation)
        {
            return GameObject.Instantiate(
                prefab,
                position,
                rotation,
                _aiHierarchy.SpawnContainer);
        }
                
        public void DestroyAIView(AIView view, int destroyDelay)
        {
            if(view == null)
                return;
                    
            GameObject.Destroy(view, destroyDelay);
        }

#endregion

        private Transform GetRandomSpawnPoint()
        {
            var spawnPoints =_aiHierarchy.SpawnPoints;
            var randomIndex = Random.Range(0, spawnPoints.Count);
            return spawnPoints[randomIndex];
        }

        private int curIndex = 0;
        private string GenerateAIName(string configName)
        {
             var newName = $"{configName} ID:{++curIndex}";
             return newName;
        }
    }
}