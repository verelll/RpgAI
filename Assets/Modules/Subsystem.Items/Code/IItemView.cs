using System;

namespace Test.Items
{
    public interface IItemView
    {
        public void InitView(ItemInstance itemInstance);
        public void DisposeView(Action destroyCallback = null);
    }
}