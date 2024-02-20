using SecurityAffairs.Files.Scripts;
using SecurityAffairs.Scripts.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private Animator _resolutionsTextAnimator;

    [SerializeField]
    private Text _resolutionsText;

    [SerializeField]
    private AudioSource _clockAudioSource;

    [SerializeField]
    private GameObject _findableImagesListGO;

    [SerializeField]
    private Sprite _selectedSprite;

    [SerializeField]
    private Sprite _unselectedSprite;

    public override void InstallBindings()
    {
        Container.Bind<IResolutionsService>()
            .To<ResolutionsService>()
            .AsSingle()
            .WithArguments(_resolutionsTextAnimator, _resolutionsText);
        Container.Bind<SelectablesManager>().AsSingle();
        Container.Bind<IFindablesService>()
            .To<FindablesService>()
            .AsSingle()
            .WithArguments(_findableImagesListGO, _selectedSprite, _unselectedSprite);
        Container.Bind<IAudioService>()
            .To<AudioService>()
            .AsSingle()
            .WithArguments(_clockAudioSource); 

    }
}