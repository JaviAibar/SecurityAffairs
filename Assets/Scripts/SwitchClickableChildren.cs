using UnityEngine;

public class SwitchClickableChildren : MonoBehaviour
{
    [SerializeField] private Selectable _selectable;
    private void Awake()
    {
        if (_selectable == null)  
            _selectable = GetComponentInChildren<Selectable>();
    }

    public void EnableClicable()
    {
        _selectable.EnableClicable();
    }

    public void DisableClicable()
    {
        _selectable.DisableClicable();
    }
}
