using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour {
    Engine engine;
    public bool found = false;
    private bool isClicable;

    void Start()
    {
        engine = GameObject.FindGameObjectWithTag("Engine").GetComponent<Engine>();
    }

    public void Found()
    {
        print("isClicable: " + isClicable + ", found: " + found + ", faltan: " + (engine.findings < engine.findable.Count));
        if (isClicable && !found && engine.findings < engine.findable.Count)
        {
            found = true;
            engine.findable[engine.findings++].sprite = engine.selected;
        }
    }

    public void SwitchClicable()
    {
        isClicable = !isClicable;
        print("Ahora clicable es: " + isClicable);
    }
}
