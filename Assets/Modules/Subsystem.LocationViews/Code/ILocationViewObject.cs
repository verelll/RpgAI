using System;
using UnityEngine;

namespace Test.LocationView
{
    public interface ILocationViewObject
    {
        GameObject GetView { get; }

        event Action<ILocationViewObject> OnDestroyed;
    }
}
