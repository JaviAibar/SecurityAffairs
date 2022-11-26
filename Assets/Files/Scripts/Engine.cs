using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Engine : MonoBehaviour {

    public Image cursor;
    public Vector3 pos;
    public int findings = 0;
    public List<Image> findable;
    public Sprite selected;
    public Sprite unselected;
    private float secs = 0;
    private List<Selectable> selectables = new List<Selectable>();
    public GameObject canvas;
    public Animator[] animators;

    public AudioSource clock;
	// Use this for initialization
	void Start ()
    {
        Screen.SetResolution(Screen.width, (int)(Screen.width / 1.77f), FullScreenMode.FullScreenWindow);
        Cursor.visible = false;
        GameObject[] gameobjectsFindable = GameObject.FindGameObjectsWithTag("Findable");
        GameObject[] gameobjectsSelectable = GameObject.FindGameObjectsWithTag("Selectable");
        foreach (GameObject go in gameobjectsFindable)
        {
            findable.Add(go.GetComponent<Image>());
        }
        foreach (GameObject go in gameobjectsSelectable)
        {
            selectables.Add(go.GetComponent<Selectable>());
        }
        animators = canvas.GetComponentsInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //pos = Input.mousePosition;
        cursor.transform.position = Input.mousePosition;
        secs += Time.deltaTime;
        if (secs > 10)
        {
            secs = 0;
            findings = 0;
            ResetFindables();
        }
        if (findings == selectables.Count)
        {
            print("Finaliza el juego");
        }
	}

    private void ResetFindables()
    {
        foreach (Image i in findable) i.sprite = unselected;
        foreach (Selectable s in selectables) s.found = false;

    }

    public void init()
    {
        clock.Play();
        foreach (Animator a in animators)
        {
            a.SetTrigger("start");
            secs = 0;
        }
    }


}
