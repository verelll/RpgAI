using UnityEngine;
using UnityEngine.AI;

namespace Test.AI
{
    public class AIBehaviour : MonoBehaviour
    {
        [Header("Main Settings")]
        [SerializeField]
        private NavMeshAgent navMeshAgent;

        [SerializeField] 
        private MeshRenderer meshRenderer;

        [Header("Weapon Settings")] 
        [SerializeField]
        private Transform itemSlot;

        public NavMeshAgent Agent => navMeshAgent;

        public void SetMaterial(Material material)
        {
            meshRenderer.material = material;
        }
    }
}