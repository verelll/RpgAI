using System;
using Test.AI;
using Test.Items;
using UnityEngine;

namespace Test.Game
{
    public class GameCheats : MonoBehaviour
    {
        [SerializeField]
        private GameStarter starter;

        [SerializeField, Header("Items Cheats")]
        private ItemConfig spawnedItem;
        
        public static event Action<Vector3> OnMouseClick;
        
        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     foreach (var ai in aiManager.AIControllers)
            //     {
            //       
            //     }
            // }

            // //Move Click Point
            // if (Input.GetMouseButtonDown(0))
            // {
            //     var clickPos = GetMouseClickPoint();
            //     CreatedDebugPoint(clickPos);
            //     OnMouseClick?.Invoke(clickPos);
            // }
            //
            // //Spawn in click pos
            if (Input.GetMouseButtonDown(1))
            {
                var aiManager = starter.ModulesContainer.GetManager<AIManager>();
            
                var clickPos = GetMouseClickPoint();
            
                var aiController = aiManager.SpawnRandomAI(clickPos);
            }
            
            //Spawn Item
            if (Input.GetKeyDown(KeyCode.I))
            {
                var itemsManager = starter.ModulesContainer.GetManager<ItemsManager>();
                itemsManager.CreateItemInRandomPoint(itemsManager.GetRandomItemConfig());
            }
        }

        private Vector3 GetMouseClickPoint()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider != null)
                    return hit.point;
            }

            return default;
        }

        private void CreatedDebugPoint(Vector3 position)
        {
            var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.name = "ObjectTarget";
            obj.transform.position = position;
            Destroy(obj, 2);
        }
    }
}