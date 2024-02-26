using NUnit.Framework;
using SecurityAffairs.Testing;
using SecurityAffairs.Tests;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;

internal class TestSelectablesManagerPlay : ZenjectIntegrationTestFixture
{
    Selectable[] selectables;
    GameObject findableGO;

    [SetUp]
    public void CommonInstall()
    {
        findableGO = SetupScenario.FindablesSetup();

        Assert.NotNull(findableGO);
    }


    [UnityTest]
    public IEnumerator _00_TestSelectablesManagerAllSelectablesFound()
    {
        PreInstall();

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

        PostInstall();

        var selectablesManager = Container.Resolve<SelectablesManager>();
        IFindablesService findablesService = Container.Resolve<IFindablesService>();

        Assert.NotNull(findablesService);
        Assert.AreEqual(0, findablesService.Founds);

        // If we find a selectable (whichever it is)
        for (int i = 0; i < selectables.Length; i++)
        {
            selectables[i].SwitchClicable();
            selectables[i].FindSelectable();
        }

        bool loadedScene = false;
        SceneManager.sceneLoaded += (_, _) => {
            loadedScene = true;
        };

        while(!loadedScene) yield return null;
        
        var nam = SceneManager.GetActiveScene().name;
        Assert.AreEqual("End", nam);
    }
}
