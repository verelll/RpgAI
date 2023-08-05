using Test.Architecture;
using UnityEngine;

namespace Test.Cameras
{
    [CreateAssetMenu(
        fileName = "CameraSettings", 
        menuName = "Camera/CameraSettings", 
        order = 10)]
    public class CameraSettings : SingletonScriptableObject<CameraSettings>
    {
        [Header("Main Settings")]
        public CameraHierarchy cameraPrefab;

        [Header("Move Settings")]
        public float moveSpeed = 5f;
        
        [Header("Rotation Settings")]
        public float rotationSpeed = 0.2f;
        public KeyCode leftRotationButton;
        public KeyCode rightRotationButton;

        // [Header("Zoom Settings")] 
        // public float minZoom;
        // public float maxZoom;
        // public float defaultZoom;
        // public float zoomSpeed;
    }
}