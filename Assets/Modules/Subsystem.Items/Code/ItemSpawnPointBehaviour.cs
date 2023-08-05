using UnityEngine;

namespace Test.Items
{
    public class ItemSpawnPointBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Transform spawnPointTransform;

        public Transform SpawnPointTransform => spawnPointTransform;

        public IItemView SpawnedItem;

        public bool IsEmpty => SpawnedItem == null;
    }
}