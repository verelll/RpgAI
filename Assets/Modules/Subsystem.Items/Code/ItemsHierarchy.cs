using System.Collections.Generic;
using UnityEngine;

namespace Test.Items
{
    public class ItemsHierarchy : MonoBehaviour
    {
        [SerializeField] private Transform itemsContainer;
        [SerializeField] private List<ItemSpawnPoint> itemSpawnPoints;

        public Transform ItemsContainer => itemsContainer;
        public List<ItemSpawnPoint> ItemSpawnPoints => itemSpawnPoints;
    }
}