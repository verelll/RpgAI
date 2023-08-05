using Test.Architecture;
using UnityEngine;

namespace Test.AI.States
{
    [CreateAssetMenu(
        menuName = "States/LookPassiveStateSettings",
        fileName = "LookPassiveStateSettings",
        order = 10)]
    public class LookPassiveStateSettings : SingletonScriptableObject<LookPassiveStateSettings>
    {
        public LookPassiveStateBehaviour lookStatePrefab;
        //
        // public Vector3 lookCenterField;
        // public Vector3 lookSizeField;
    }
}