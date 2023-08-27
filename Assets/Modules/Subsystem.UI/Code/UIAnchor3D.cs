using Test.Architecture;
using Test.Cameras;
using UnityEngine;

namespace Test.UI
{
    public sealed class UIAnchor3D : MonoBehaviour
    {
        private Camera _uiCamera;
        private Camera _mainCamera;

        public Vector3 ScreenPosition
        {
            get
            {
                CheckMainCamera();
                var pos = _mainCamera.WorldToScreenPoint(transform.position);

                return pos;
            }
        }
        
        public Vector3 UICanvasPosition
        {
            get
            {
                CheckUICamera();
                var pos = _uiCamera.ScreenToWorldPoint(ScreenPosition);

                return pos;
            }
        }
        
        private void CheckMainCamera()
        {
            if (_mainCamera != null)
                return;
            
            _mainCamera = GlobalModulesContainer.Instance.GetManager<CameraManager>().CurrentCameraController.MainCamera;
        }

        private void CheckUICamera()
        {
            if(_uiCamera != null)
                return;
                
            _uiCamera = GlobalModulesContainer.Instance.GetManager<UIManager>().Hierarchy.UICamera;
        }
    }
}