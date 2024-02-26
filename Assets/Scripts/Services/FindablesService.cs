using UnityEngine;
using UnityEngine.UI;

public class FindablesService : IFindablesService
{
    public int Founds => _findings;

    private int _findings = 0;
    private Image[] _findableImagesList;

    private GameObject _findableImagesListGO;
    private Sprite _selectedSprite;
    private Sprite _unselectedSprite;

    public FindablesService(GameObject findableImagesListGO, Sprite selectedSprite, Sprite unselectedSprite)
    {
        _findableImagesListGO = findableImagesListGO;
        _selectedSprite = selectedSprite;
        _unselectedSprite = unselectedSprite;
        ResetFindables();
    }

    public void ResetFindables()
    {
        Init();
        _findings = 0;
        foreach (Image i in _findableImagesList) i.sprite = _unselectedSprite;
    }

    public void SelectableFound()
    {
        _findableImagesList[_findings++].sprite = _selectedSprite;
    }

    private void Init()
    {
        _findableImagesList = _findableImagesListGO.GetComponentsInChildren<Image>();
    }
}
