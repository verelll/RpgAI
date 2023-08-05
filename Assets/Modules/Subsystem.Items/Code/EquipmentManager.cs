using System.Collections.Generic;
using Test.AI;
using Test.Architecture;
using UnityEngine;

namespace Test.Items
{
    public sealed class EquipmentManager : ManagerBase
    {
        private Dictionary<string, ItemView> _itemsView;

        private AIManager _aiManager;
        private ItemsManager _itemsManager;

        public override void InitDependencyManagers()
        {
            _aiManager = GetManager<AIManager>();
            _itemsManager = GetManager<ItemsManager>();
        }

        public override void Init()
        {
            _itemsView = new Dictionary<string, ItemView>();
        }

        public override void Dispose()
        {
            
        }

        public void PickUpItem(string itemID, string aiID)
        {
            var itemInstance = _itemsManager.GetByID(itemID);
            var aiController = _aiManager.GetByID(aiID);
            
            if(itemInstance == null || aiController == null)
                return;

            var itemType = itemInstance.Config.itemType;
            switch (itemType)
            {
                case ItemType.Weapon:
                    PickUpWeaponItem(itemInstance, aiController);
                    break;
                case ItemType.Health:
                    PickUpHealthItem(itemInstance, aiController);
                    break;
                default:
                    break;
            }
        }

        public void DropItem(ItemInstance itemInstance, Vector3 position)
        {
            var itemType = itemInstance.Config.itemType;
            switch (itemType)
            {
                case ItemType.Weapon:
                    DestroyWeaponView(itemInstance);
                    break;
                default:
                    break;
            }
        }

#region Weapon

        private void PickUpWeaponItem(ItemInstance itemInstance, AIController aiController)
        {
           
        }

        private void CreateWeaponView(ItemInstance itemInstance, AIController aiController)
        {
            
        }
        
        private void DestroyWeaponView(ItemInstance itemInstance)
        {
            
        }

#endregion


#region Health

        private void PickUpHealthItem(ItemInstance itemInstance, AIController aiController)
        {
            Debug.Log($"[HealthUse] Add heath to {aiController.ID}");
        }

#endregion

    }
}