using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Test.AI.States;
using Test.Architecture;
using UnityEngine;

namespace Test.AI
{
    [CreateAssetMenu(
        fileName = "AIPresetConfig",
        menuName = "AI/AIPresetConfig",
        order = 10)]
    public class AIPresetConfig : MultitonScriptableObjectsByName<AIPresetConfig>
    {
        [BoxGroup("Main Settings")] public Color color;
        
        [BoxGroup("Move Settings")] public float moveSpeed = 3.5f;
        [BoxGroup("Move Settings")] public float angularSpeed = 120;
        [BoxGroup("Move Settings")] public float acceleration = 8;
        [BoxGroup("Move Settings")] public float stoppingDistance = 0;

        [BoxGroup, SerializeField, ValueDropdown("@GetStates(states)")] private string startStateName;
        [PropertySpace(10)]
        [BoxGroup, SerializeField] private List<BaseAIStateSettings> states = new();
        
        public string ID => name;
        public string StartState => startStateName;
        public IEnumerable<BaseAIStateSettings> States => states;

        private IEnumerable GetStates(List<BaseAIStateSettings> states)
        {
            if (states == null)
                return default;

            var cachedIdList = new ValueDropdownList<string>();
            cachedIdList.Add("", "");

            foreach (var state in states)
                cachedIdList.Add(state.ID, state.ID);

            return cachedIdList;
        }
    }

}