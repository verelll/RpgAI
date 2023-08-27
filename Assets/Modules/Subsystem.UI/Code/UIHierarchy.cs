using UnityEngine;

namespace Test.UI
{
    public class UIHierarchy : MonoBehaviour
    {
        [SerializeField]
        private Canvas uiCanvas;

        [SerializeField]
        private Camera uiCamera;

        [SerializeField] 
        private Transform statsLayer;

        public Canvas UICanvas => uiCanvas;
        public Camera UICamera => uiCamera;

        public Transform StatsLayer => statsLayer;
    }
}
