using System;
using System.Collections;
using Test.LocationView;
using UnityEngine;

namespace Test.Items
{
    public class ItemView : MonoBehaviour, IItemView, ILocationViewObject
    {
        [Header("Idle Settings"), SerializeField]
        private Vector3 idleAnimAxis;
        
        public ItemInstance ItemInstance { get; private set; }
        
        private IEnumerator _activeAnim;
        
        public void InitView(ItemInstance instance)
        {
            ItemInstance = instance;
            PlayIdleAnimation();
        }

        public void DisposeView(Action destroyCallback)
        {
            OnDestroyed?.Invoke(this);
            StopIdleAnimation();
            destroyCallback?.Invoke();
        }

#region Animations

        private void PlayIdleAnimation()
        {
            _activeAnim = IdleAnimation();
            StartCoroutine(_activeAnim);
        }

        private void StopIdleAnimation()
        {
            StopCoroutine(_activeAnim);
        }

        private IEnumerator IdleAnimation()
        {
            while (true)
            {
                transform.Rotate(idleAnimAxis);
                yield return null;
            }
        }
        
#endregion


#region ILocationView

        public GameObject GetView => gameObject;
        public event Action<ILocationViewObject> OnDestroyed;

#endregion

    }
}