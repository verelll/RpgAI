using UnityEngine;

namespace Test.Cameras
{
    public class CameraHierarchy : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private Transform pivot;
        [SerializeField] private Transform cameraTransform;

        public Camera Cam => camera;
        public Transform Pivot => pivot;
        public Transform CameraTransform => cameraTransform;
    }
}