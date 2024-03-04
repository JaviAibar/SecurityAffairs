using NUnit.Framework;
using SecurityAffairs.Testing;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[TestFixture]
public class TestFindableService : ZenjectUnitTestFixture
{
    Sprite spriteSelected;
    Sprite spriteUnselected;
    Selectable[] selectables;
    Transform findableObjects;
    Image[] findableImages;
    readonly SelectablesManager selectablesManager;

    [SetUp]
    public void CommonInstall()
    {
        spriteSelected = SetupScenario.MockSelectedSprite;
        spriteUnselected = SetupScenario.MockSelectedSprite;
        selectables = SetupScenario.SelectableSetup();

        Assert.AreNotEqual(spriteSelected, spriteUnselected);
        Assert.NotNull(selectables);
        var findableGO = SetupScenario.FindablesSetup();
        findableObjects = findableGO.transform;
        Assert.NotNull(findableObjects);
        findableImages = GetFindableImages(findableObjects);
        Container.Bind<IFindablesService>()
            .To<FindablesService>()
            .AsSingle()
            .WithArguments(findableGO, spriteSelected, spriteUnselected);
        Container.Bind<SelectablesManager>().AsSingle();

        Container.Bind<GameObject>().AsSingle();
    }

    [Test]
    public void _00_TestFounds()
    {
        IFindablesService findablesService = Container.Resolve<IFindablesService>();
        var selectablesManager = Container.Resolve<SelectablesManager>();
        Assert.NotNull(findablesService);
        Assert.AreEqual(0, findablesService.Founds);

        // If we find a selectable (whichever it is)
        selectables[1].EnableClicable();
        selectables[1].FindSelectable();

        // ...then first image should be selected
        Assert.AreEqual(1, findablesService.Founds);


        // Repeat
        selectables[2].EnableClicable();
        selectables[2].FindSelectable();

        Assert.AreEqual(2, findablesService.Founds);

        findablesService.ResetFindables();
        Assert.AreEqual(0, findablesService.Founds);

        selectables[0].EnableClicable();
        selectables[0].FindSelectable();
        Assert.AreEqual(1, findablesService.Founds);

        selectables[1].FindSelectable();
        // Should not count, because it was already found
        Assert.AreEqual(1, findablesService.Founds);

        selectables[1].ResetFindable();
        selectables[1].FindSelectable();
        // Should not count because, at reset, should be not clicable
        Assert.AreEqual(1, findablesService.Founds);

        selectables[1].EnableClicable();
        selectables[1].FindSelectable();
        Assert.AreEqual(2, findablesService.Founds);
    }


    [Test]
    public void _01_TestImagesChanges()
    {
        var selectablesManager = Container.Resolve<SelectablesManager>();
        var findablesService = Container.Resolve<IFindablesService>();
        Assert.NotNull(findablesService);
        Assert.AreEqual(0, findablesService.Founds);
        // Every findable image should start as unselected
        for (int i = 0; i < findableImages.Length; i++)
            Assert.AreEqual(spriteUnselected, findableImages[i].sprite);

        // If we find a selectable (whichever it is)
        selectables[1].EnableClicable();
        selectables[1].FindSelectable();

        // ...then first image should be selected
        Assert.AreEqual(spriteSelected, findableImages[0].sprite);

        // ... subsequent, though, should still be unselected
        for (int i = 1; i < findableImages.Length; i++)
            Assert.AreEqual(spriteUnselected, findableImages[i].sprite);

        // Repeat
        selectables[2].EnableClicable();
        selectables[2].FindSelectable();

        for (int i = 0; i < findableImages.Length - 1; i++)
            Assert.AreEqual(spriteSelected, findableImages[i].sprite);

        Assert.AreEqual(spriteUnselected, findableImages[2].sprite);
    }


    private Image[] GetFindableImages(Transform findableObjects)
    {
        Image[] result = new Image[findableObjects.childCount];
        for (int i = 0; i < findableObjects.childCount; i++)
        {
            result[i] = findableObjects.GetChild(i).GetComponent<Image>();
        }
        return result;
    }
}