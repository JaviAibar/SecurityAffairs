using ModestTree;
using UnityEngine;
using UnityEngine.UI;

namespace SecurityAffairs.Testing
{
    internal class SetupScenario
    {

        public static GameObject FindablesSetup()
        {
            var findableObjectsGO = new GameObject();
            var element1 = new GameObject();
            var element2 = new GameObject();
            var element3 = new GameObject();
            element1.AddComponent<Image>();
            element2.AddComponent<Image>();
            element3.AddComponent<Image>();
            var findablePrefab = SecurityAffairs.Tests.PrefabFinder.GetPrefab("FindablePrefab");
            Assert.IsNotNull(findablePrefab);
            GameObject.Instantiate(findablePrefab, findableObjectsGO.transform);
            GameObject.Instantiate(findablePrefab, findableObjectsGO.transform);
            GameObject.Instantiate(findablePrefab, findableObjectsGO.transform);
            return findableObjectsGO;
        }

        public static Selectable[] SelectableSetup()
        {
            var selectablesGameObject = new GameObject();
             var selectableGO1 = new GameObject();
             var selectable1 = selectableGO1.AddComponent<Selectable>();
             selectableGO1.transform.parent = selectablesGameObject.transform;

             var selectableGO2 = new GameObject();
             var selectable2 = selectableGO2.AddComponent<Selectable>();
             selectableGO2.transform.parent = selectablesGameObject.transform;

             var selectableGO3 = new GameObject();
             var selectable3 = selectableGO3.AddComponent<Selectable>();
             selectableGO3.transform.parent = selectablesGameObject.transform;

            return new Selectable[] { selectable1, selectable2, selectable3 };
        }
    }
}
