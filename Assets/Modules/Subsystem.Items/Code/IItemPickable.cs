using System;

namespace Test.Items
{
    public interface IItemPickable
    {
        public event Action OnPickUp;
        
        void PickUp();
    }
}