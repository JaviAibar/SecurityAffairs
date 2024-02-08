using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Engine : MonoBehaviour {

    public Image cursor;
    public Vector3 pos;
    public int findings = 0;
    public List<Image> findable;
    public Sprite selected;
    public Sprite unselected;
    private float secs = 0;
    public float Secs => secs;
    private List<Selectable> selectables = new List<Selectable>();
    private List<Resolution> _resolutions;
    private int _selectedRes = 0;
    public GameObject canvas;
    public Animator[] animators;
    public Text resText;

    public AudioSource clock;
	// Use this for initialization
	void Start ()
    {
        _resolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == 60).ToList(); 
        _selectedRes = _resolutions.Count - 1;
        Debug.Log($"Detected {_resolutions.Count} resolutions:"+ string.Join(",",_resolutions.Select(e => $"({e.height}, {e.width})").ToList()) );
        SetRes();
        //Screen.SetResolution(Screen.width, (int)(Screen.width / 1.77f), FullScreenMode.FullScreenWindow);
        if (SceneManager.GetActiveScene().name == "Game") Cursor.visible = false;
        else Cursor.visible = true;
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
        if (secs >= 10)
        {
            findings = 0;
            ResetFindables();
            init();
        }
        if (findings == selectables.Count)
        {
            SceneManager.LoadScene("End");
        }
	}

    private void ResetFindables()
    {
        foreach (Selectable s in selectables) s.found = false;
        foreach (Image i in findable) i.sprite = unselected;
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

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator SetRes()
    {
        Resolution res = _resolutions[_selectedRes];
        print($"Setting {(_selectedRes + 1)} res out of {_resolutions.Count}: selected {res.width} x {res.height}");
        Screen.SetResolution(res.width, res.height, FullScreenMode.FullScreenWindow);
        resText.gameObject.SetActive(true);
        resText.text = $"{res.width} x {res.height}";
        yield return new WaitForSeconds(1.5f);
        resText.gameObject.SetActive(false);
    }

    public void SetNextRes()
    {
        if (_selectedRes < _resolutions.Count - 1) ++_selectedRes;
        StartCoroutine(SetRes());
    }
    
    public void SetPrevRes()
    {
        if (_selectedRes > 0) --_selectedRes;
        StartCoroutine(SetRes());
    }
}
