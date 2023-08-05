using System;
using System.Collections.Generic;
using System.Linq;
using Test.Architecture;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Test.Items
{
    public sealed class ItemsManager : ManagerBase
    {
        private Dictionary<string, ItemInstance> _items;

        public ItemsHierarchy ItemsHierarchy { get; private set; }
        
        public event Action<ItemInstance> OnItemInstanceCreated;
        public event Action<ItemInstance> OnItemInstanceDestroyed;
        
        public event Action<IItemView> OnItemViewCreated;
        public event Action<IItemView> OnItemViewPreDestroyed;
        
        public event Action<ItemInstance, GameObject> OnItemUsed;
        
        public override void Init()
        {
            _items = new Dictionary<string, ItemInstance>();
            ItemsHierarchy = GameObject.FindObjectOfType<ItemsHierarchy>();
            
            ItemConfig.Init();
        }
        

#region Spawn Item

        public ItemInstance CreateItemInRandomPoint(ItemConfig config)
        {
            var spawnTransform = GetRandomSpawnPoint();
            if (spawnTransform == null)
                return default;
            
            var item = CreateItemInstance(config, spawnTransform.SpawnPointTransform.position);
            spawnTransform.SpawnedItem = item.View;
            return item;
        }

        private ItemSpawnPointBehaviour GetRandomSpawnPoint()
        {
            var spawnPoints = ItemsHierarchy.ItemSpawnPoints.Where(p => p.IsEmpty).ToList();
            if (spawnPoints.Count == 0)
                return default;
            
            var randomIndex = Random.Range(0, spawnPoints.Count);
            return spawnPoints[randomIndex];
        }
        
        public ItemConfig GetRandomItemConfig()
        {
            var items = ItemConfig.Objects;
            var randomIndex = Random.Range(0, items.Count);
            return items[randomIndex];
        }

#endregion


#region Create Item

        public ItemInstance CreateItemInstance(ItemConfig config, Vector3 position)
        {
            var generatedID = GenerateItemID(config);
            var itemInstance = new ItemInstance(config, generatedID);

            CreateItemView(
                itemInstance, 
                position  + config.spawnPositionOffset,
                Quaternion.Euler(config.spawnRotationOffset),
                ItemsHierarchy.ItemsContainer);
            itemInstance.Init();
            
            _items.Add(generatedID, itemInstance);
            OnItemInstanceCreated?.Invoke(itemInstance);
            return itemInstance;
        }

        public ItemView CreateItemView(
            ItemInstance itemInstance,
            Vector3 position,
            Quaternion rotation,
            Transform parent = null)
        {
            var prefab = itemInstance.Config.itemPrefab;
            var curParent = parent == null 
                ? ItemsHierarchy.ItemsContainer 
                : parent;
            
            var createdView = CreateItemView(
                itemInstance,
                prefab,
                position,
                rotation,
                curParent,
                itemInstance.ID);

            return createdView;
        }

        public ItemEquippedView CreateItemEquippedView(
            ItemInstance itemInstance,
            Vector3 position,
            Quaternion rotation,
            Transform parent)
        {
            var prefab = itemInstance.Config.itemEquippedPrefab;
            var createdView = CreateItemView(
                itemInstance,
                prefab,
                position,
                rotation,
                parent,
                itemInstance.ID);
            
            return createdView;
        }

        private T CreateItemView<T>(
            ItemInstance itemInstance,
            T prefab,
            Vector3 position,
            Quaternion rotation,
            Transform parent,
            string named)
            where T : MonoBehaviour, IItemView
        {
            var createdView = GameObject.Instantiate(prefab, position, rotation, parent);
            
            itemInstance.SetView(createdView);
            createdView.InitView(itemInstance);
            createdView.name = named;
            OnItemViewCreated?.Invoke(createdView);
            return createdView;
        }

        #endregion


#region Delete Item

        public void DestroyItemInstance(string itemID)
        {
            var itemInstance = GetByID(itemID);
            if(itemInstance == null)
                return;
            
            DestroyItemView(itemInstance);
            OnItemInstanceDestroyed?.Invoke(itemInstance);
            itemInstance.Dispose();
            _items.Remove(itemID);
        }

        public void DestroyItemView(ItemInstance itemInstance)
        {
            if (itemInstance.View == null)
            {
                Debug.Log($"[ItemsManager] Item: {itemInstance.ID}. Behaviour already deleted!");
                return;
            }
            
            OnItemViewPreDestroyed?.Invoke(itemInstance.View);
            itemInstance.View?.DisposeView();
            itemInstance.SetView(null);

            GameObject.Destroy((MonoBehaviour) itemInstance.View);
        }
        
#endregion


#region Get Items

        public ItemInstance GetByID(string itemID)
        {
            if (!_items.TryGetValue(itemID, out var itemInstance))
            {
                Debug.Log($"[ItemsManager] Item with id: {itemID} not found!");
                return default;
            }

            return itemInstance;
        }

#endregion


#region ID Generator

        private int curGeneratedIndex = 0;
        private string GenerateItemID(ItemConfig itemConfig)
        {
            var newId = $"{itemConfig.name} ID:{++curGeneratedIndex}";
            return newId;
        }

#endregion

    }
}
