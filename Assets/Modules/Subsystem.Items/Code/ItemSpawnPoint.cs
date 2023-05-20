using UnityEngine;

namespace Test.Items
{
    public class ItemSpawnPoint : MonoBehaviour
    {
        [SerializeField]
        private Transform spawnPointTransform;

        public Transform SpawnPointTransform => spawnPointTransform;

        public ItemBehaviour SpawnedItem;

        public bool IsEmpty => SpawnedItem == null;
    }
}