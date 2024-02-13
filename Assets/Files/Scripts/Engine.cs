using Assets.Files.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Engine : MonoBehaviour {

    [SerializeField] private Image cursor;
    [SerializeField] private Vector3 pos;
    [SerializeField] private int findings = 0;
    [SerializeField] private List<Image> findable;
    [SerializeField] private Sprite selected;
    [SerializeField] private Sprite unselected;
    private bool _initiated = false;
    private float _seconds = 0;
    public float Secs => _seconds;
    private List<Selectable> selectables = new List<Selectable>();
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Animator[] _animators;
    private const float MaxTimeToFind = 10.0f;
    public bool TimeOut => _seconds >= MaxTimeToFind;


    [SerializeField] private AudioSource clock;
	// Use this for initialization
	void Start ()
    {
        //Screen.SetResolution(Screen.width, (int)(Screen.width / 1.77f), FullScreenMode.FullScreenWindow);
        if (SceneManager.GetActiveScene().name == "Game") Cursor.visible = false;
        else Cursor.visible = true;

        GameObject[] gameobjectsFindable = GameObject.FindGameObjectsWithTag("Findable");
        findable.AddRange(gameobjectsFindable.Select(go => go.GetComponent<Image>()));

        GameObject[] gameobjectsSelectable = GameObject.FindGameObjectsWithTag("Selectable");
        selectables.AddRange(gameobjectsSelectable.Select(go => go.GetComponent<Selectable>()));

        foreach (Selectable selectable in selectables)
        {
            selectable.OnFound += OnSelectableFound;
        }

        _animators = _canvas.GetComponentsInChildren<Animator>();
	}

    private void OnSelectableFound(Selectable selectable)
    {
        findable[findings++].sprite = selected;
        if (findings == selectables.Count)
            SceneManager.LoadScene(Constants.End);
    }

    // Update is called once per frame
    void Update () {
        if (!_initiated) return;
        //pos = Input.mousePosition;
        cursor.transform.position = Input.mousePosition;
        _seconds += Time.deltaTime;
        if (TimeOut) Init();
    }

    private void ResetFindables()
    {
        findings = 0;
        foreach (Selectable s in selectables) s.Found = false;
        foreach (Image i in findable) i.sprite = unselected;
    }

    public void Init()
    {
        _initiated = true;
        ResetFindables();

        clock.Play();
        _seconds = 0;
        foreach (Animator animator in _animators)
            animator.SetTrigger("start");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
