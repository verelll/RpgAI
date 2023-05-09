using System;
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
        public ItemType itemType;
        
    }

    public enum ItemType
    {
        None = 0,
        Health = 50,
        Weapon = 100,
    }

    public class ItemInstance
    {
        private ItemConfig _config;
        private ItemBehaviour _behaviour;
        
        private ItemInstance(ItemConfig config, ItemBehaviour behaviour)
        {
            _config = config;
            _behaviour = behaviour;
        }
    }

    public class ItemBehaviour : MonoBehaviour
    {
        public event Action OnPickUp;

        public void InvokePickUp() => OnPickUp?.Invoke();
    }
}