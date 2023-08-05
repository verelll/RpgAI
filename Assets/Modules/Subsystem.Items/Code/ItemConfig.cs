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

        public ItemView itemPrefab;
        public ItemEquippedView itemEquippedPrefab;

        public Vector3 spawnPositionOffset;
        public Vector3 spawnRotationOffset;

        [Space] 
        public Vector3 equipPositionOffset;
        public Vector3 equipRotationOffset;
    }

    public enum ItemType
    {
        None = 0,
        Health = 50,
        Weapon = 100,
    }
}