using Test.Architecture;
using UnityEngine;

namespace Test.Cameras
{
    public class CameraController 
    {
        private CameraHierarchy _cameraHierarchy;
        private CameraSettings _settings;

        private float _horizontal;
        private float _vertical;
        private float _mouseWheel;
        
        //private float _curZoom;

#region Init

        public void Init()
        {
            _settings = CameraSettings.Instance;

            SetDefaultZoom();
            
            UnityEventProvider.OnUpdate += HandleUpdate;
        }
        
        public void Dispose()
        {
            UnityEventProvider.OnUpdate -= HandleUpdate;
        }

        private void SetDefaultZoom()
        {
            // _curZoom = _settings.defaultZoom;
        }
        
#endregion


#region Update

        private void HandleUpdate()
        {
            UpdatePosition();
            UpdateRotation();
            UpdateZoom();
        }

        private void UpdatePosition()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");

            var direction = new Vector3(_horizontal, 0, _vertical);
            _cameraHierarchy.transform.Translate(direction * _settings.moveSpeed * Time.deltaTime);
        }

        private void UpdateRotation()
        {
            var curLocalRotation = _cameraHierarchy.transform.localRotation.eulerAngles;
            var yChangeValue = _settings.rotationSpeed * Time.deltaTime;
            
            if (Input.GetKey(_settings.leftRotationButton))
                _cameraHierarchy.transform.localRotation = Rotate(curLocalRotation.y + yChangeValue);
            
            if (Input.GetKey(_settings.rightRotationButton))
                _cameraHierarchy.transform.localRotation = Rotate(curLocalRotation.y - yChangeValue);
            
            Quaternion Rotate(float yRot) => Quaternion.Euler(new Vector3(0, yRot, 0));
        }

        private void UpdateZoom()
        {
            // _mouseWheel = Input.GetAxis("Mouse ScrollWheel");
            // if(_mouseWheel == 0)
            //     return;
            //
            // _curZoom -= _mouseWheel;
            //
            // _curZoom = Mathf.Clamp(_curZoom, _settings.minZoom, _settings.maxZoom);
            // Debug.Log($"CurZoom: {_curZoom}");
            //
            // float yPos = 0;
            // if(_mouseWheel < 0)
            //     yPos = Mathf.Lerp(_curZoom, _settings.maxZoom, _curZoom * _settings.zoomSpeed * Time.deltaTime);
            //
            // if(_mouseWheel > 0)
            //     yPos = Mathf.Lerp(_curZoom, _settings.minZoom, _curZoom * _settings.zoomSpeed * Time.deltaTime);
            //
            // var localPosition = _cameraHierarchy.CameraTransform.localPosition;
            // var curZoomPos = new Vector3(localPosition.x, yPos, localPosition.z);
            // _cameraHierarchy.CameraTransform.localPosition = curZoomPos;
        }

#endregion


#region Constructor

        public CameraController(CameraHierarchy hierarchy)
        {
            _cameraHierarchy = hierarchy;
        }

#endregion

    }
}
