using System;
using System.Collections;
using Test.LocationView;
using UnityEngine;

namespace Test.AI.States
{
    public class LookPassiveStateBehaviour : MonoBehaviour
    {
        public event Action<ILocationViewObject> OnObjectEnter;
        
        public event Action<ILocationViewObject> OnObjectExit;

        private void OnTriggerEnter(Collider other)
        {
            var collisionObject = other?.GetComponent<ILocationViewObject>();
            if(collisionObject == null)
                return;
            
            collisionObject = other?.GetComponentInParent<ILocationViewObject>();
            if(collisionObject == null)
                return;
            
            collisionObject = other?.GetComponentInChildren<ILocationViewObject>();
            if(collisionObject == null)
                return;
            
            OnObjectEnter?.Invoke(collisionObject);
        }
        
        private void OnTriggerExit(Collider other)
        {
            var collisionObject = other?.GetComponent<ILocationViewObject>();
            if(collisionObject == null)
                return;
            
            collisionObject = other?.GetComponentInParent<ILocationViewObject>();
            if(collisionObject == null)
                return;
            
            collisionObject = other?.GetComponentInChildren<ILocationViewObject>();
            if(collisionObject == null)
                return;
            
            OnObjectExit?.Invoke(collisionObject);
        }
    }
}