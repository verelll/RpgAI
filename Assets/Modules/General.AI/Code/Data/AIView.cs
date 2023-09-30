using System;
using Test.Effects;
using Test.LocationView;
using Test.Stats;
using Test.UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

namespace Test.AI
{
    public class AIView : MonoBehaviour, ILocationViewObject, IStatProvider
    {
        [Header("Main Settings")]
        [SerializeField]
        private NavMeshAgent navMeshAgent;

        [SerializeField] 
        private BottleEffects effectsComponent;

        [SerializeField] 
        private UIAnchor3D anchor;

        [SerializeField] 
        private VisualEffect effect;

        public BottleEffects EffectsComponent => effectsComponent;
        
        public NavMeshAgent Agent => navMeshAgent;

        public UIAnchor3D Anchor => anchor;

        public VisualEffect Effect => effect;

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