using Test.Architecture;
using UnityEngine;

namespace Test.UI
{
    public sealed class UIManager : ManagerBase
    {
        private UIHierarchy _hierarchy;
        private Camera _uiCamera;

        public UIHierarchy Hierarchy
        {
            get
            {
                if (_hierarchy == null)
                    _hierarchy = GameObject.FindObjectOfType<UIHierarchy>();

                return _hierarchy;
            }
        }
    }
}
