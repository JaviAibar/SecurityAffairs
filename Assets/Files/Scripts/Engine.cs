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

    [SerializeField] private Image _cursor;
    [SerializeField] private int _findings = 0;
    private Image[] _findableImagesList;
    [SerializeField] private GameObject _findableImagesListGO;
    [SerializeField] private Sprite _selectedSprite;
    [SerializeField] private Sprite _unselectedSprite;
    private bool _initiated = false;
    private float _seconds = 0;
    public float Seconds => _seconds;
    private Selectable[] _selectables;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Animator[] _animators;
    private const float MaxTimeToFind = 10.0f;
    public bool TimeOut => _seconds >= MaxTimeToFind;


    [SerializeField] private AudioSource _clockAudioSource;
	// Use this for initialization
	void Start ()
    {
        if (SceneManager.GetActiveScene().name == "Game") Cursor.visible = false;
        else Cursor.visible = true;

        _findableImagesList = _findableImagesListGO.GetComponentsInChildren<Image>();

        _selectables = FindObjectsByType<Selectable>(FindObjectsSortMode.None);

        foreach (Selectable selectable in _selectables)
            selectable.OnFound += OnSelectableFound;

        _animators = _canvas.GetComponentsInChildren<Animator>();
	}

    private void OnSelectableFound(Selectable selectable)
    {
        _findableImagesList[_findings++].sprite = _selectedSprite;
        if (_findings == _selectables.Length)
            SceneManager.LoadScene(Constants.End);
    }

    // Update is called once per frame
    void Update () {
        if (!_initiated) return;

        _cursor.transform.position = Input.mousePosition;
        _seconds += Time.deltaTime;
        if (TimeOut) Init();
    }

    private void ResetFindables()
    {
        _findings = 0;
        foreach (Selectable s in _selectables) s.Found = false;
        foreach (Image i in _findableImagesList) i.sprite = _unselectedSprite;
    }

    public void Init()
    {
        _initiated = true;
        ResetFindables();

        _clockAudioSource.Play();
        _seconds = 0;
        foreach (Animator animator in _animators)
            animator.SetTrigger("start");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
