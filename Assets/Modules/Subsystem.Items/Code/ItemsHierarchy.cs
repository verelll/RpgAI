using System.Collections.Generic;
using UnityEngine;

namespace Test.Items
{
    public class ItemsHierarchy : MonoBehaviour
    {
        [SerializeField] private Transform itemsContainer;
        [SerializeField] private List<ItemSpawnPointBehaviour> itemSpawnPoints;

        public Transform ItemsContainer => itemsContainer;
        public List<ItemSpawnPointBehaviour> ItemSpawnPoints => itemSpawnPoints;
    }
}