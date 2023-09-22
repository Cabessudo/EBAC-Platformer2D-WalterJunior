using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPause : MonoBehaviour
{
    public GameObject menuScreen;
    public bool pause = false;

    public void Pause(bool b)
    {
        pause = b;

        if(pause)
        menuScreen.SetActive(true);
        else
        menuScreen.SetActive(false);
    }
}
