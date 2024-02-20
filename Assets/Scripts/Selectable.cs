using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour {
    private bool _found = false;
    private bool isClicable;

    public bool Found => _found;

    public event Action<Selectable> OnFound;

   public void FindSelectable()
    {
        //print(name + " isClicable: " + isClicable + ", found: " + found + ", faltan: " + (engine.findings < engine.findable.Count) + " "+ engine.Secs);
        if (isClicable && !_found)
        {
            _found = true;
            OnFound?.Invoke(this);
        }
    }
   
    public void SwitchClicable()
    {
        isClicable = !isClicable;
    }

    internal void Reset()
    {
        _found = false;
    }
}
