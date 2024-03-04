using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class CursorManager : ITickable
{
    readonly Image _cursor;

    public CursorManager(Image cursor)
    {
        _cursor = cursor;
        if (SceneManager.GetActiveScene().name == "Game") Cursor.visible = false;
        else Cursor.visible = true;
    }

    public void Tick()
    {
        _cursor.transform.position = Input.mousePosition;
    }
    
}
