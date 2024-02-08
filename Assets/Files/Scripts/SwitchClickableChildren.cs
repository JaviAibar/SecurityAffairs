using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchClickableChildren : MonoBehaviour
{
    private Selectable s;
    private void Awake()
    {
        s = GetComponentInChildren<Selectable>();
    }

    public void SwitchClickable()
    {
        s.SwitchClicable();
    }
}
