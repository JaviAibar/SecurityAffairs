using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;


public class SelectablesManager
{
    private IFindablesService _findablesService;

    private Selectable[] _selectables;

    [Inject]
    public void Construct(IFindablesService findablesService)
    {
        _findablesService = findablesService;
        Init();
    }

    private void Init()
    {
        _selectables = Object.FindObjectsByType<Selectable>(FindObjectsSortMode.None);
        foreach (Selectable selectable in _selectables)
            selectable.OnFound += OnSelectableFound;
        ResetSelectables();
    }

    public void ResetSelectables()
    {
        foreach (Selectable s in _selectables) s.ResetFindable();
        _findablesService.ResetFindables();
    }

    public void OnSelectableFound(Selectable selectable)
    {
        _findablesService.SelectableFound();
        if (_findablesService.Founds == _selectables.Length)
            SceneManager.LoadScene(Constants.End);
    }
}

