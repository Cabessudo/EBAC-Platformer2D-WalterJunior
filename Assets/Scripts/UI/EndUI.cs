using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndUI : MonoBehaviour
{
    public TextMeshProUGUI endCoinText;
    public SOInt coins;

    // Update is called once per frame
    void Update()
    {
        endCoinText.SetText(coins.value + " x");
    }
}
