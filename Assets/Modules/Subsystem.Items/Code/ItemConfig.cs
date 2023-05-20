using Test.Architecture;
using UnityEngine;

namespace Test.Items
{
    [CreateAssetMenu(
        fileName = "ItemConfig", 
        menuName = "Items/ItemConfig", 
        order = 10)]
    public class ItemConfig : MultitonScriptableObjectsByName<ItemConfig>
    {
        [Header("Main Settings")]
        public ItemType itemType;

        public ItemBehaviour itemPrefab;

        public Vector3 spawnPositionOffset;
        public Vector3 spawnRotationOffset;
    }

    public enum ItemType
    {
        None = 0,
        Health = 50,
        Weapon = 100,
    }
}