using System;
using Test.LocationView;
using UnityEngine;
using UnityEngine.AI;

namespace Test.AI
{
    public class AIBehaviour : MonoBehaviour, ILocationViewObject
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

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }

#region ILocationView

        public GameObject GetView => this.gameObject;
        public event Action<ILocationViewObject> OnDestroyed;

#endregion

    }
}