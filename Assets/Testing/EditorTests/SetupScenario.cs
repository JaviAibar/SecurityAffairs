using NUnit.Framework;
using UnityEngine;

namespace SecurityAffairs.Testing
{
    public class SetupScenario
    {
        private static Sprite CreateDefault => Sprite.Create(Texture2D.blackTexture, new Rect(0.0f, 0.0f, 1, 1), Vector2.zero);
        public static Sprite MockSelectedSprite
        {
            get
            {
                var spriteSelected = CreateDefault;
                spriteSelected.name = "Selected";
                Assert.IsNotNull(spriteSelected);
                return spriteSelected;
            }
        }

        public static Sprite MockUnselectedSprite
        {
            get
            {
                var spriteUnselected = CreateDefault;
                spriteUnselected.name = "Unselected";
                Assert.IsNotNull(spriteUnselected);
                return spriteUnselected;
            }
        }

        /// <summary>
        /// Instantiates necessary gameobjects
        /// </summary>
        /// <returns>findableGO parent of 3 FindablePrefabs</returns>
        public static GameObject FindablesSetup()
        {
            var findableObjectsGO = new GameObject();
            var findablePrefab = SecurityAffairs.Tests.PrefabFinder.GetPrefab("FindablePrefab");
            Assert.IsNotNull(findablePrefab);

            GameObject.Instantiate(findablePrefab, findableObjectsGO.transform);
            Assert.AreEqual(1, findableObjectsGO.transform.childCount);

            GameObject.Instantiate(findablePrefab, findableObjectsGO.transform);
            Assert.AreEqual(2, findableObjectsGO.transform.childCount);

            GameObject.Instantiate(findablePrefab, findableObjectsGO.transform);
            Assert.AreEqual(3, findableObjectsGO.transform.childCount);

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
