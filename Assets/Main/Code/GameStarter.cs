using System;
using System.Collections;
using Test.AI;
using Test.Architecture;
using Test.Cameras;
using Test.Items;
using UnityEngine;

namespace Test.Game
{
    public class GameStarter : MonoBehaviour
    {
        public ModulesContainer ModulesContainer { get; private set; }

        private IEnumerator Start()
        {
            ModulesContainer = new ModulesContainer();

            ModulesContainer.AddManager<CameraManager>();
            ModulesContainer.AddManager<AIManager>();
            ModulesContainer.AddManager<ItemsManager>();

            yield return null;

            ModulesContainer.InitManagers();
        }

        private void OnDestroy()
        {
            ModulesContainer?.DisposeManagers();
        }
    }
}