using System;
using UnityEngine;

namespace Test.LocationView
{
    public class ExampleLocationView : MonoBehaviour, ILocationViewObject
    {
        public GameObject GetView => gameObject;
        
        public event Action<ILocationViewObject> OnDestroyed;

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}