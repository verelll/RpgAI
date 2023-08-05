using System.Collections.Generic;
using Test.AI.States;
using Test.AI.Triggers;
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
            return CreateAI(presetConfig, Vector3.zero, Quaternion.identity);
        }
        
        public AIController CreateAI(AIPresetConfig presetConfig, Vector3 position)
        {
            return CreateAI(presetConfig, position, Quaternion.identity);
        }

        private AIController CreateAI(AIPresetConfig presetConfig, Vector3 position, Quaternion rotation)
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
            var behaviour = CreateAIBehaviour(
                    _settings.aiPrefab,
                    curPos,
                    curRot);

            var id = GenerateAIName(presetConfig.ID);
            behaviour.gameObject.name = id;

            var controller = new AIController(id, model, behaviour, presetConfig, _settings.aiMaterial);
            MContainer.Inject(controller);
            _aiList.Add(id, controller);
            
            CreateInternalController(controller);
            return controller;
        }

        //Переписать потом
        private void CreateInternalController(AIController controller)
        {
            controller.AddInternalController(new AIStatesInternalController(controller));
            controller.AddInternalController(new AITriggerInternalController(controller));
        }

        private AIModel CreateAIModel()
        {
            return new AIModel();
        }
        
        private AIBehaviour CreateAIBehaviour(AIBehaviour prefab, Vector3 position, Quaternion rotation)
        {
            return GameObject.Instantiate(
                prefab,
                position,
                rotation,
                _aiHierarchy.SpawnContainer);
        }

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