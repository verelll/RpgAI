using System;
using UnityEngine;

namespace Test.Items
{
    public class ItemEquippedView : MonoBehaviour, IItemView
    {
        public void InitView(ItemInstance itemInstance) { }

        public void DisposeView(Action destroyCallback) { }
    }
}