using SecurityAffairs.Files.Scripts;
using SecurityAffairs.Scripts.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class Engine : MonoBehaviour {

    [SerializeField] private Image _cursor;
    private bool _initiated = false;
    private float _seconds = 0;
    public float Seconds => _seconds;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Animator[] _animators;
    private const float MaxTimeToFind = 10.0f;
    public bool TimeOut => _seconds >= MaxTimeToFind;
    [Inject] private IAudioService _audioService;
    [Inject] private SelectablesManager _selectablesManager;
	void Start ()
    {
        if (SceneManager.GetActiveScene().name == "Game") Cursor.visible = false;
        else Cursor.visible = true;

        _animators = _canvas.GetComponentsInChildren<Animator>();
	}

    // Update is called once per frame
    void Update () {
        if (!_initiated) return;

        _cursor.transform.position = Input.mousePosition;
        _seconds += Time.deltaTime;
        if (TimeOut) Init();
    }
    public void Init()
    {
        _initiated = true;
        _selectablesManager.ResetSelectables();
        _audioService.StartPlaying();
        _seconds = 0;
        foreach (Animator animator in _animators)
            animator.SetTrigger("start");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
