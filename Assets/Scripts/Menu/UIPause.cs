using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class UIPause : Singleton<UIPause>
{
    public CursorManager cursor;
    public GameObject menuScreen;
    private KeyCode _keyPause = KeyCode.Escape;
    public bool pause = false;

    void Update()
    {
        if(Input.GetKeyDown(_keyPause))
        Pause();
    }

    public void Pause(bool b = true)
    {
        pause = b;

        if(pause)
        {
            menuScreen.SetActive(true);
            Time.timeScale = 0;
            cursor.FreeCursor();
        }   
        else
        {
            menuScreen.SetActive(false);
            Time.timeScale = 1;
            cursor.LockCursor();
        }
        
    }
}
