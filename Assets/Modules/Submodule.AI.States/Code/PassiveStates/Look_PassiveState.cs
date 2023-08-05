using System;
using System.Collections.Generic;
using Test.LocationView;
using UnityEngine;

namespace Test.AI.States
{
    public class Look_PassiveState : BaseAIState
    {
        public List<ILocationViewObject> ObjectsInSeeRange { get; private set; }

        public event Action<ILocationViewObject> OnSeeSomethingInRange;

        private LookPassiveStateBehaviour _lookPassiveStateBehaviour;
        private BoxCollider _collider;
        private GameObject _stateParent;

        private LookPassiveStateSettings _settings;

        public override void Init()
        {
            _settings = LookPassiveStateSettings.Instance;
            ObjectsInSeeRange = new List<ILocationViewObject>();
            
            _lookPassiveStateBehaviour = GameObject.Instantiate(_settings.lookStatePrefab);
            _lookPassiveStateBehaviour.transform.SetParent(DataController.Behaviour.transform, false);

            _lookPassiveStateBehaviour.OnObjectEnter += HandleObjectEnter;
            _lookPassiveStateBehaviour.OnObjectExit += HandleObjectExit;
        }

        public override void Dispose()
        {
            _lookPassiveStateBehaviour.OnObjectEnter -= HandleObjectEnter;
            _lookPassiveStateBehaviour.OnObjectExit -= HandleObjectExit;
        }

#region Handlers

        private void HandleObjectEnter(ILocationViewObject locationViewObject)
        {
            if(ObjectsInSeeRange.Contains(locationViewObject))
                return;

            locationViewObject.OnDestroyed += HandleObjectDestroyed;
            ObjectsInSeeRange.Add(locationViewObject);
            OnSeeSomethingInRange?.Invoke(locationViewObject);
            
            ObjectEnter(locationViewObject);
        }

        private void HandleObjectExit(ILocationViewObject locationViewObject)
        {
            if (!ObjectsInSeeRange.Contains(locationViewObject)) 
                return;
            
            locationViewObject.OnDestroyed -= HandleObjectDestroyed;
            ObjectsInSeeRange.Remove(locationViewObject);

            ObjectExit(locationViewObject);
        }

        private void HandleObjectDestroyed(ILocationViewObject locationViewObject)
        {
            locationViewObject.OnDestroyed -= HandleObjectDestroyed;
            ObjectsInSeeRange.Remove(locationViewObject);
            ObjectExit(locationViewObject);
        }
        
#endregion


#region Virtual Handlers

        protected virtual void ObjectEnter(ILocationViewObject locationViewObject) { }

        protected virtual void ObjectExit(ILocationViewObject locationViewObject) { }

#endregion

    }
}