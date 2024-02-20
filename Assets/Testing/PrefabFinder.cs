using ModestTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Assert.IsNotNull(prefabsComponent, "Expected to find prefab '{0}'", name);
            return selectedPrefab;
        }
    }
}
