using Zenject;
using SecurityAffairs.Scripts.Services;
using NUnit.Framework;
using UnityEngine;
using SecurityAffairs.Testing;

[TestFixture]
public class TestFindableService : ZenjectUnitTestFixture
{
    Sprite spriteSelected;
    Sprite spriteUnselected;
    Selectable[] selectables;


    [SetUp]
    public void CommonInstall()
    {
        spriteSelected = Sprite.Create(null, Rect.zero, Vector2.zero);
        spriteUnselected = Sprite.Create(null, Rect.zero, Vector2.zero);
        //selectables = SetupScenario.SelectableSetup();
        var findableObjectsGO = SetupScenario.FindablesSetup();
        Container.Bind<IFindablesService>().To<FindablesService>().AsSingle().WithArguments(findableObjectsGO, spriteSelected, spriteUnselected);
        Container.Bind<SelectablesManager>().AsSingle();
    }


    

    [Test]
    //[UnityTest]
    public /*IEnumerator*/ void RunTest1()
    {
        var selectablesManager = Container.Resolve<SelectablesManager>();
        var findableService = Container.Resolve<IFindablesService>();

        Assert.AreEqual(0, findableService.Founds);

        selectables[0].FindSelectable();
        Assert.AreEqual(1, findableService.Founds);

        selectables[0].FindSelectable();
        Assert.AreEqual(1, findableService.Founds);

        selectables[2].FindSelectable();
        Assert.AreEqual(2, findableService.Founds);

        findableService.ResetFindables();
        Assert.AreEqual(0, findableService.Founds);
        //yield break;
    }
}