using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour {
    private bool found = false;
    private bool isClicable;

    public bool Found { get => found; set => found = value; }

    public event Action<Selectable> OnFound;

   public void FindSelectable()
    {
        //print(name + " isClicable: " + isClicable + ", found: " + found + ", faltan: " + (engine.findings < engine.findable.Count) + " "+ engine.Secs);
        if (isClicable && !found)
        {
            found = true;
            OnFound?.Invoke(this);
        }
    }
   
    public void SwitchClicable()
    {
        isClicable = !isClicable;
    }
}
