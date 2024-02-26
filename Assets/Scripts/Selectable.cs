using System;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    private bool _found = false;
    private bool _isClicable;
    public bool IsClicable => _isClicable;

    public bool Found => _found;

    public event Action<Selectable> OnFound;

    public void FindSelectable()
    {
        //print(name + " isClicable: " + isClicable + ", found: " + found + ", faltan: " + (engine.findings < engine.findable.Count) + " "+ engine.Secs);
        if (_isClicable && !_found)
        {
            _found = true;
            OnFound?.Invoke(this);
        }
    }

    public void SwitchClicable()
    {
        _isClicable = !_isClicable;
    }

    public void ResetFindable()
    {
        _found = false;
    }
}
