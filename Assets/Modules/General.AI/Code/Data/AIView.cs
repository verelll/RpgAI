using System;
using Test.LocationView;
using Test.Stats;
using Test.UI;
using UnityEngine;
using UnityEngine.AI;

namespace Test.AI
{
    public class AIView : MonoBehaviour, ILocationViewObject, IStatProvider
    {
        [Header("Main Settings")]
        [SerializeField]
        private NavMeshAgent navMeshAgent;

        [SerializeField] 
        private MeshRenderer meshRenderer;

        [SerializeField] 
        private UIAnchor3D anchor;

        public NavMeshAgent Agent => navMeshAgent;

        public UIAnchor3D Anchor => anchor;

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


#region IStatProvider

        public UIAnchor3D ProviderAnchor => anchor;

#endregion
    }
}