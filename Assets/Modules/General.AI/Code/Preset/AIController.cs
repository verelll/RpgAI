using System;
using System.Collections.Generic;
using Test.States;
using UnityEngine;

namespace Test.AI
{
    public class AIController: IStateController
    {
        public AIModel Model { get; }
        public AIBehaviour Behaviour { get; }
        public AIPresetConfig Config { get; }

        public Dictionary<Type, BaseAIState> AllStates { get; }
        
        public BaseAIState ActiveState { get; private set; }
        
        public AIController(AIModel model, AIBehaviour behaviour, AIPresetConfig config)
        {
            Model = model;
            Behaviour = behaviour;
            Config = config;

            //Set material
            behaviour.SetMaterial(config.mainMaterial);
            
            //Set params
            var agent = Behaviour.Agent;
            agent.speed = Config.moveSpeed;
            agent.angularSpeed = Config.angularSpeed;
            agent.acceleration = Config.acceleration;
            agent.stoppingDistance = Config.stoppingDistance;

            AllStates = new Dictionary<Type, BaseAIState>();
        }

        public T AddState<T>(bool initState) where T : BaseAIState, new()
        {
            var state = GetState<T>();
            if (state != null)
                return state;
            
            var newState = new T();
            var type = newState.GetType();

            if(initState)
                newState.Init(this);
            
            AllStates[type] = newState;
            return newState;
        }

        public void RemoveState<T>() where T : BaseAIState
        {
            var state = GetState<T>();
            state?.Dispose();
        }

        public void RemoveAllStates()
        {
            foreach (var pair in AllStates)
                pair.Value.Dispose();
            
            AllStates.Clear();
        }

        public T GetState<T>() where T : BaseAIState
        {
            var type = typeof(T);
            if (!AllStates.TryGetValue(type, out var state))
                return default;

            return state as T;
        }

        public void ActivateState<T>(Action callback = null) where T : BaseAIState
        {
            ActiveState?.EndState();

            var state = GetState<T>();
            if(state == null)
            {
                Debug.Log("[AIController] State not added!");
                callback?.Invoke();
                return;
            }

            ActiveState = state;
            state.OnStateEnd += HandleStateEnd;

            void HandleStateEnd()
            {
                state.OnStateEnd -= HandleStateEnd;
                callback?.Invoke();
                ActiveState = null;
            }
        }
    }
}