using System;
using System.Collections.Generic;

namespace Test.Architecture
{
    public interface IInternalDataContainer
    {
        Dictionary<Type, IInternalData> InternalDatas { get; }

        public T GetInternalData<T>() where T : IInternalData;

        public void AddInternalData<T>() where T : IInternalData, new();
        
        public void RemoveInternalData<T>() where T : IInternalData;
    }
}