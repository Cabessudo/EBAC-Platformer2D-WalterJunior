using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI coinText;

    public static void UpdateCoins(int coin)
    {
        Instance.coinText.SetText(coin + " x");
    }
}
