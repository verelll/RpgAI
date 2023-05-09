using System.Collections.Generic;
using Test.Architecture;
using UnityEngine;
using UnityEngine.AI;

namespace Test.AI
{
    public class AIManager : ManagerBase
    {
        private List<AIController> _aiList;

        public List<AIController> AIControllers => _aiList;

        private AIHierarchy _aiHierarchy;
        private MainAISettings _settings;

        public override void InitDependencyManagers()
        {
            
        }

        public override void Init()
        {
            AIPresetConfig.Init();

            _aiList = new List<AIController>();
            
            _aiHierarchy = GameObject.FindObjectOfType<AIHierarchy>();
            _settings = MainAISettings.Instance;
        }

        public override void Dispose()
        {
            
        }

        public void SpawnRandomAI(Vector3 position)
        {
            var randomIndex = Random.Range(0, _settings.presetSpawnList.Count);
            var randomPreset = _settings.presetSpawnList[randomIndex];
            CreateAI(randomPreset, position);
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

            behaviour.gameObject.name = presetConfig.ID;
            
            var controller = new AIController(model, behaviour, presetConfig);
            _aiList.Add(controller);
            return controller;
        }

        private AIModel CreateAIModel()
        {
            var newModel = new AIModel();
            return newModel;
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
    }
}