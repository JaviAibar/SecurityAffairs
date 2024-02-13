using Assets.Files.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private Animator resolutionsTextAnimator;

    [SerializeField]
    private Text resolutionsText;

    public override void InstallBindings()
    {
        Container.Bind<IResolutionsService>()
            .To<ResolutionsService>()
            .AsSingle()
            .WithArguments(resolutionsTextAnimator, resolutionsText);

    }
}