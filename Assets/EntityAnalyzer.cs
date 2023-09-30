#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sadalmalik.Analyze
{
    public static class EntityAnalyzer
    {
        [MenuItem(itemName: "[ANALYZE]/Analyze active scenes")]
        private static void Analyze()
        {
            var counters                 = new Dictionary<int, int>();
            var uniqueComponents         = new HashSet<Type>();
            var uniqueComponentsOnObject = new HashSet<Type>();

            int minDepth = int.MaxValue;
            int maxDepth = 0;

            int maxUniquePerObject = 0;
            int maxCopiedPerObject = 0;

            var scenes = GetAllScenes();
            IterateObjectsInScenes(scenes, Handle);

            void Handle(int depth, GameObject o)
            {
                minDepth = Mathf.Min(minDepth, depth);
                maxDepth = Mathf.Max(maxDepth, depth);

                var components = o.GetComponents<Component>();
                uniqueComponentsOnObject.Clear();
                var count = components.Length;
                for (int i = 0; i < count; i++)
                {
                    var compType = components[i].GetType();
                    uniqueComponents.Add(compType);
                    uniqueComponentsOnObject.Add(compType);
                }

                var unique = uniqueComponentsOnObject.Count;
                maxUniquePerObject = Mathf.Max(maxUniquePerObject, unique);
                maxCopiedPerObject = Mathf.Max(maxCopiedPerObject, count - unique);

                if (!counters.ContainsKey(count))
                    counters[count] = 0;
                counters[count]++;
            }

            float componentsMinCount     = float.MaxValue;
            float componentsMaxCount     = 0;
            float componentsAverageCount = 0;
            float componentsMedianCount  = FindMedian(counters);

            var totalAmount = 0;
            foreach (var pair in counters)
            {
                var componentsCount = pair.Key;
                var objectsCount    = pair.Value;
                componentsMinCount = Mathf.Min(componentsMinCount, componentsCount);
                componentsMaxCount = Mathf.Max(componentsMaxCount, componentsCount);

                componentsAverageCount += componentsCount * objectsCount;
                totalAmount            += objectsCount;
            }
            componentsAverageCount /= totalAmount;

            var results = new StringBuilder();
            results.AppendLine("Project statistics:");
            results.AppendLine($"  Total Unique components: {uniqueComponents.Count}");
            results.AppendLine($"  Max Unique per object: {maxUniquePerObject}");
            results.AppendLine($"  Max Copied per object: {maxCopiedPerObject}");
            results.AppendLine($"  Min per Object: {componentsMinCount}");
            results.AppendLine($"  Max per Object: {componentsMaxCount}");
            results.AppendLine($"  Average count: {componentsAverageCount}");
            results.AppendLine($"  Median count: {componentsMedianCount}");
            results.AppendLine($"  Min Depth: {minDepth}");
            results.AppendLine($"  Max Depth: {maxDepth}");
            Debug.Log(results.ToString());
        }

        private static float FindMedian(Dictionary<int, int> counters)
        {
            var totalAmount = 0;
            var keys        = new HashSet<int>();
            foreach (var pair in counters)
            {
                keys.Add(pair.Key);
                totalAmount += pair.Value;
            }

            var keysList = new List<int>(keys);
            keysList.Sort();
            var center = totalAmount / 2;
            totalAmount = 0;
            for (int i = 0; i < keysList.Count; i++)
            {
                var key = keysList[i];
                totalAmount += counters[key];
                if (center < totalAmount)
                    return key;

                if (center == totalAmount && i < keysList.Count - 1)
                    return 0.5f * (key + keysList[i + 1]);
            }

            return keysList[keysList.Count - 1];
        }

        private static void IterateObjectsInScenes(List<Scene> scenes, Action<int, GameObject> handler)
        {
            for (int i = 0; i < scenes.Count; i++)
            {
                var objects = scenes[i].GetRootGameObjects();

                for (int k = 0; k < objects.Length; k++)
                {
                    IterateObjects(objects[k], handler);
                }
            }
        }

        private static void IterateObjects(GameObject o, Action<int, GameObject> handler, int depth = 0)
        {
            if (o == null) return;
            handler(depth, o);
            foreach (Transform child in o.transform)
            {
                IterateObjects(child.gameObject, handler, depth + 1);
            }
        }

        private static List<Scene> GetAllScenes()
        {
            int     countLoaded  = SceneManager.sceneCount;
            Scene[] loadedScenes = new Scene[countLoaded];

            for (int i = 0; i < countLoaded; i++)
            {
                loadedScenes[i] = SceneManager.GetSceneAt(i);
            }

            return new List<Scene>(loadedScenes);
        }
    }
}

#endif