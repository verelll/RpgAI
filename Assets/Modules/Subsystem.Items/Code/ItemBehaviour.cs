using System;
using System.Collections;
using UnityEngine;

namespace Test.Items
{
    public class ItemBehaviour : MonoBehaviour
    {
        [Header("Idle Settings")]
        [SerializeField]
        private Vector3 idleAnimAxis;
        
        private ItemInstance _itemInstance;
        
        public event Action<ItemInstance> OnPickUp;

        public void InvokePickUp() => OnPickUp?.Invoke(_itemInstance);

        private IEnumerator _activeAnim;
        
        public void InitBehaviour(ItemInstance instance)
        {
            _itemInstance = instance;
            PlayIdleAnimation();
        }

        public void DisposeBehaviour(Action destroyCallback)
        {
            StopIdleAnimation();
            destroyCallback?.Invoke();
        }

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
    }
}