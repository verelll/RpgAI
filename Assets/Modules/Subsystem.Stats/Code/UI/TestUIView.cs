using UnityEngine;
using UnityEngine.EventSystems;

namespace Test.Stats
{
    public class TestUIView : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"[Click] Object: {gameObject.name}");
        }
    }
}