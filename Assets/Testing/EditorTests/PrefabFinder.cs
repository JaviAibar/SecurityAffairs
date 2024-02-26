using ModestTree;
using UnityEngine;

namespace SecurityAffairs.Tests
{
    public static class PrefabFinder
    {
        public static GameObject GetPrefab(string name)
        {
            var prefabsComponent = ((GameObject)Resources.Load("Prefabs")).GetComponent<PrefabsScript>();
            GameObject selectedPrefab = null;
            foreach (var prefab in prefabsComponent.prefabs)
            {
                if (prefab.name == name)
                {
                    selectedPrefab = prefab;
                    break;
                }
            }
            Assert.IsNotNull(prefabsComponent, "Prefab container not found in Resources folder. Is it named Prefabs?");
            Assert.IsNotNull(selectedPrefab, "Expected to find prefab '{0}'. Is it registered in Resources/Prefabs?", name);
            return selectedPrefab;
        }
    }
}
