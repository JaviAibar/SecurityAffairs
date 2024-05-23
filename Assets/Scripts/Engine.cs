using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Engine : MonoBehaviour
{
    private bool _initiated = false;
    private float _seconds = 0;
    public float Seconds => _seconds;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Animator[] _animators;
    private const float MaxTimeToFind = 10.0f;
    public bool TimeOut => _seconds >= MaxTimeToFind;
    private IAudioService _audioService;
    private SelectablesManager _selectablesManager;
    private CursorManager _cursorManager;


    [Inject]
    public void Construct(CursorManager cursorManager, 
                            SelectablesManager selectablesManager, 
                            IAudioService audioService)
    {
        _cursorManager = cursorManager;
        _selectablesManager = selectablesManager;
        _audioService = audioService;
    }

    void Start()
    {
        _animators = _canvas.GetComponentsInChildren<Animator>()
                               .Where(e => e.CompareTag("SceneAnimator")).ToArray();
    }

    void Update()
    {
        if (!_initiated) return;

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
