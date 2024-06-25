using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI endCoinText;
    public SOInt coins;
    public GameObject endText;
    public GameObject gameUI;
    public void ShowEndUI()
    {
        endCoinText.SetText(coins.value + " x");
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
