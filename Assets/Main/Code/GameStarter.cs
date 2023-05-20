using System.Collections;
using Test.AI;
using Test.Architecture;
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
            ModulesContainer.InitContainer();

            ModulesContainer.AddManager<AIManager>();
            ModulesContainer.AddManager<ItemsManager>();

            yield return null;

            ModulesContainer.InitManagers();
        }
    }
}