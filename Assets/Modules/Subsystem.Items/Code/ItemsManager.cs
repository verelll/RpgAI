using System.Collections.Generic;
using System.Linq;
using Test.Architecture;
using UnityEngine;

namespace Test.Items
{
    public class ItemsManager : ManagerBase
    {
        private List<ItemInstance> _items;

        private ItemsHierarchy _itemsHierarchy;
        
        public override void InitDependencyManagers() { }

        public override void Init()
        {
            _items = new List<ItemInstance>();
            _itemsHierarchy = GameObject.FindObjectOfType<ItemsHierarchy>();
            
            ItemConfig.Init();
        }

        public override void Dispose()
        {
            
        }

#region Spawn Item

        public ItemInstance CreateItemInRandomPoint(ItemConfig config)
        {
            var spawnTransform = GetRandomSpawnPoint();
            var item = CreateItemInstance(config, spawnTransform.SpawnPointTransform.position);
            spawnTransform.SpawnedItem = item.Behaviour;
            return item;
        }

        private ItemSpawnPoint GetRandomSpawnPoint()
        {
            var spawnPoints = _itemsHierarchy.ItemSpawnPoints.Where(p => p.IsEmpty).ToList();
            var randomIndex = Random.Range(0, spawnPoints.Count);
            return spawnPoints[randomIndex];
        }

#endregion


#region Create Item

        public ItemInstance CreateItemInstance(ItemConfig config, Vector3 position)
        {
            var itemInstance = new ItemInstance(config);

            var behaviour = CreateItemBehaviour(config, position, _itemsHierarchy.ItemsContainer);
            behaviour.InitBehaviour(itemInstance);
            
            itemInstance.SetBehaviour(behaviour);
            itemInstance.Init();
            
            _items.Add(itemInstance);
            return itemInstance;
        }

        public ItemBehaviour CreateItemBehaviour(ItemConfig config, Vector3 position, Transform parent)
        {
            var createdBehaviour = GameObject.Instantiate(
                config.itemPrefab, 
                position + config.spawnPositionOffset, 
                Quaternion.Euler(config.spawnRotationOffset),
                parent);
            
            return createdBehaviour;
        }

#endregion

#region Delete Item

        public void DestroyItemInstance(ItemInstance itemInstance)
        {
            DestroyItemBehaviour(itemInstance);
            itemInstance.Dispose();
            _items.Remove(itemInstance);
        }

        public void DestroyItemBehaviour(ItemInstance itemInstance)
        {
            if (itemInstance.Behaviour == null)
            {
                Debug.Log($"[ItemsManager] Item: {itemInstance.Config.name}. Behaviour already deleted!");
                return;
            }
            
            itemInstance.Behaviour.DisposeBehaviour(HandleDisposed);

            void HandleDisposed()
            {
                GameObject.Destroy(itemInstance.Behaviour.gameObject);
                itemInstance.SetBehaviour(null);
            }
        }

#endregion


    }
}
