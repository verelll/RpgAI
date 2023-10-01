using System;
using UnityEngine;

namespace Test.Water3D
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class Water3D_PhysicsObject : MonoBehaviour
    {
        [SerializeField] 
        private float _drag = 2;
        
        [SerializeField] 
        private float _angularDrag = 0.07f;

        [Space]
        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField] 
        private Vector3 _customScale;

        private float _defaultDrag;
        private float _defaultAngularDrag;

        public bool InWater { get; set; }

        public Vector3 ObjectScale
        {
            get
            {
                var scale = _customScale;
                if (scale == Vector3.zero)
                    scale = transform.localScale;

                return scale;
            }
        }

        private void Start()
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();

            _defaultDrag = _rigidbody.drag;
            _defaultAngularDrag = _rigidbody.angularDrag;
        }

        public void AddWaterForce(Vector3 force)
        {
            _rigidbody.AddForce(force);
        }

        public void UpdateDiveDrag()
        {
            _rigidbody.drag = InWater 
                ? _drag 
                : _defaultDrag;
                
            _rigidbody.angularDrag = InWater 
                ? _angularDrag 
                : _defaultAngularDrag;
        }
    }
}