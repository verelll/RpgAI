using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Test.Water3D
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class Water3D_PhysicsSimulator : MonoBehaviour
    {
        [SerializeField] 
        private float _waterDencity = 10f;
        
        [SerializeField, ReadOnly]
        private List<Water3D_PhysicsObject> _objectsInsideWater;

        private BoxCollider _waterTriggerCollider;
        
        private void Start()
        {
            _objectsInsideWater = new List<Water3D_PhysicsObject>();

            _waterTriggerCollider ??= GetComponent<BoxCollider>();
            _waterTriggerCollider.isTrigger = true;
        }

        
        private void FixedUpdate()
        {
            var waterHeight = transform.position.y + transform.localScale.y * 0.5f;
            
            for (var i = 0; i < _objectsInsideWater.Count; i++)
            {
                var physicsObject = _objectsInsideWater[i];

                var divePercent = waterHeight + physicsObject.ObjectScale.x * 0.5f;
                divePercent = Mathf.Clamp01(divePercent);
                
                physicsObject.AddWaterForce(Vector3.up * divePercent * _waterDencity);
                physicsObject.UpdateDiveDrag();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var physicsObject = other.GetComponent<Water3D_PhysicsObject>();
            if(physicsObject == null)
                return;

            if(_objectsInsideWater.Contains(physicsObject))
                return;

            physicsObject.InWater = true;
            _objectsInsideWater.Add(physicsObject);
        }

        private void OnTriggerExit(Collider other)
        {
            var physicsObject = other.GetComponent<Water3D_PhysicsObject>();
            if(physicsObject == null)
                return;
            
            if(!_objectsInsideWater.Contains(physicsObject))
                return;
            
            physicsObject.InWater = false;
            _objectsInsideWater.Remove(physicsObject);
            physicsObject.UpdateDiveDrag();
        }
    }
}
