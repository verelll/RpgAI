using Test.Architecture;
using UnityEngine;

namespace Test.Cameras
{
    public class CameraManager : ManagerBase
    {
        public CameraController CurrentCameraController { get; private set; }

        private CameraSettings _settings;

#region Base

        public override void Init()
        {
            FindOrCreateCameraController();
        }

        public override void Dispose()
        {
            if(CurrentCameraController == null)
                return;
            
            CurrentCameraController.Dispose();
        }

#endregion

        private void FindOrCreateCameraController()
        {
            var cameraHierarchy = GameObject.FindObjectOfType<CameraHierarchy>();
            if(cameraHierarchy == null)
                CreateCameraController(Vector3.zero, null);

            CurrentCameraController = new CameraController(cameraHierarchy);
            CurrentCameraController.Init();
        }

        private void CreateCameraController(Vector3 position, Transform parent)
        {
            var cameraHierarchy = GameObject.Instantiate(_settings.cameraPrefab, position, Quaternion.identity, parent);
            CurrentCameraController = new CameraController(cameraHierarchy);
            CurrentCameraController.Init();
        }
    }
}