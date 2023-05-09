using System.Collections.Generic;
using UnityEngine;

namespace Test.AI
{
    public class AIHierarchy : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> spawnPoints;

        [SerializeField]
        private Transform spawnContainer;

        public List<Transform> SpawnPoints => spawnPoints;
        public Transform SpawnContainer => spawnContainer;
    }
}