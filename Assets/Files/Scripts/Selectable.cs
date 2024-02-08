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
        print(name + " isClicable: " + isClicable + ", found: " + found + ", faltan: " + (engine.findings < engine.findable.Count) + " "+ engine.Secs);
        if (isClicable && !found && engine.findings < engine.findable.Count)
        {
            found = true;
            engine.findings++;
            print($"Clicado {engine.findings} que corresponde con GO {engine.findable[engine.findings].name}");
            engine.findable[engine.findings].sprite = engine.selected;
        }
    }

    public void SwitchClicable()
    {
        isClicable = !isClicable;
        print("Ahora clicable es: " + isClicable);
    }
}
