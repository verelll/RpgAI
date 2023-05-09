using System.Collections;
using Test.AI;
using Test.Architecture;
using UnityEngine;

namespace Test.Game
{
    public class GameStarter : MonoBehaviour
    {
        private ModulesContainer _modulesContainer;

        private IEnumerator Start()
        {
            _modulesContainer = new ModulesContainer();
            _modulesContainer.InitContainer();

            _modulesContainer.AddManager<AIManager>();

            yield return null;

            _modulesContainer.InitManagers();
        }
        
        private void Update()
        {
            //Move to click pos
            if (Input.GetMouseButtonDown(0))
            {
                var clickPos = GetMouseClickPoint();
                CreatedDebugPoint(clickPos);
                
                var aiManager = _modulesContainer.GetManager<AIManager>();
                foreach (var ai in aiManager.AIControllers)
                {
                    ai.MoveTo(clickPos);
                }
            }

            //Spawn in click pos
            if (Input.GetMouseButtonDown(1))
            {
                var clickPos = GetMouseClickPoint();
                
                var aiManager = _modulesContainer.GetManager<AIManager>();
                aiManager.SpawnRandomAI(clickPos);
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