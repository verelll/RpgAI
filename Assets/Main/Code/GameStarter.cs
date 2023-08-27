using System;
using System.Collections;
using Test.AI;
using Test.Architecture;
using Test.Cameras;
using Test.Items;
using Test.Stats;
using Test.UI;
using UnityEngine;

namespace Test.Game
{
    public class GameStarter : MonoBehaviour
    {
        private ModulesContainer _modulesContainer;
        private GlobalModulesContainer _globalModulesContainer;
        
        private IEnumerator Start()
        {
            _modulesContainer = new ModulesContainer();

            _globalModulesContainer = GlobalModulesContainer.Instance;
            _globalModulesContainer.SetContainer(_modulesContainer);

            _modulesContainer.AddManager<UIManager>();
            _modulesContainer.AddManager<CameraManager>();
            
            _modulesContainer.AddManager<AIManager>();
            _modulesContainer.AddManager<ItemsManager>();
            _modulesContainer.AddManager<StatsManager>();

            yield return null;

            _modulesContainer.InitManagers();
        }

        private void OnDestroy()
        {
            _modulesContainer?.DisposeManagers();
        }
    }
}