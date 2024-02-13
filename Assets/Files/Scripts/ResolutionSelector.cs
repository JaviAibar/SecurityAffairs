using Assets.Files.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResolutionSelector : MonoBehaviour
{
    private IResolutionsService resolutionsService;
    [Inject]
    public void Construct(IResolutionsService resSelector)
    {
        resolutionsService = resSelector;
    }

    public void SetNextRes()
    {
        resolutionsService.SetNextRes();
    }

    public void SetPrevRes()
    {
        resolutionsService.SetPrevRes();
    }
}
