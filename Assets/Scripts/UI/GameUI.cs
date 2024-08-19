using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public GameObject endText;
    public GameObject gameUI;
    
    public void ShowEndUI()
    {
        gameUI.SetActive(false);
        endText.SetActive(true);
    }

    public void DisableGameUI()
    {
        gameUI.SetActive(false);
    }

    public void EnableGameUI()
    {
        gameUI.SetActive(true);
    }
}
