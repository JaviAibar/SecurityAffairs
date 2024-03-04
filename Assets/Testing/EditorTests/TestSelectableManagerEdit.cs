using NUnit.Framework;
using SecurityAffairs.Testing;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[TestFixture]
public class TestSelectableManagerEdit : ZenjectUnitTestFixture
{
    Selectable[] selectables;


    [SetUp]
    public void CommonInstall()
    {
        var findableGO = SetupScenario.FindablesSetup();
        Assert.NotNull(findableGO);

        Container.Bind<IFindablesService>()
            .To<FindablesService>()
            .AsSingle()
            .WithArguments(
                findableGO,
                SetupScenario.MockSelectedSprite,
                SetupScenario.MockUnselectedSprite
            );
        Container.Bind<SelectablesManager>().AsSingle();

        Container.Bind<GameObject>().AsSingle();

        selectables = SetupScenario.SelectableSetup();
        SetupScenario.FindablesSetup();
    }



    [Test]
    public void _00_TestSwich()
    {
        Assert.IsFalse(selectables[0].IsClicable);
        Assert.IsFalse(selectables[1].IsClicable);
        Assert.IsFalse(selectables[2].IsClicable);

        foreach (var selectable in selectables)
        {
            selectable.EnableClicable();
        }

        Assert.IsTrue(selectables[0].IsClicable);
        Assert.IsTrue(selectables[1].IsClicable);
        Assert.IsTrue(selectables[2].IsClicable);

        foreach (var selectable in selectables)
        {
            selectable.ResetFindable();
        }

        Assert.IsFalse(selectables[0].IsClicable);
        Assert.IsFalse(selectables[1].IsClicable);
        Assert.IsFalse(selectables[2].IsClicable);

        foreach (var selectable in selectables)
        {
            selectable.EnableClicable();
        }

        Assert.IsTrue(selectables[0].IsClicable);
        Assert.IsTrue(selectables[1].IsClicable);
        Assert.IsTrue(selectables[2].IsClicable);
    }

    [Test]
    public void _01_TestFindSelectable()
    {
        for (int i = 0; i < selectables.Length; i++)
        {
            selectables[i].EnableClicable();
            selectables[i].FindSelectable();
            Assert.IsTrue(selectables[i].Found);
        }
    }

    [Test]
    public void _02_TestResetFindable()
    {
        // Setting one found but not the rest
        selectables[0].EnableClicable();
        selectables[0].FindSelectable();
        Assert.IsTrue(selectables[0].Found);

        for (int i = 1; i < selectables.Length; i++)
        {
            // The rest should remain false
            Assert.IsFalse(selectables[i].Found);
        }

        for (int i = 0; i < selectables.Length; i++)
        {
            // Every findable is reset
            selectables[i].ResetFindable();
            // ...therefore, each should be false
            Assert.IsFalse(selectables[i].Found);
        }
    }

    [Test]
    public void _03_TestSelectablesManagerReset()
    {
        var selectablesManager = Container.Resolve<SelectablesManager>();
        IFindablesService findablesService = Container.Resolve<IFindablesService>();

        Assert.NotNull(findablesService);
        Assert.AreEqual(0, findablesService.Founds);

        // If we find a selectable (whichever it is)
        selectables[0].EnableClicable();
        selectables[0].FindSelectable();


        selectables[1].EnableClicable();
        selectables[1].FindSelectable();

        // ...then first image should be selected
        Assert.AreEqual(2, findablesService.Founds);

        selectablesManager.ResetSelectables();

        Assert.AreEqual(0, findablesService.Founds);
        Assert.IsFalse(selectables[0].Found);
        Assert.IsFalse(selectables[1].Found);
    }
}