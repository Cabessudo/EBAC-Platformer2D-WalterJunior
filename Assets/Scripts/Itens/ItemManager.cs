using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class ItemManager : Singleton<ItemManager>
{ 
    public int coins;

    void Start()
    {
        Restart();
    }

    void Restart()
    {
        coins = 0;
        UpdateUI();
    }

    public void AddCoins(int manager = 1)
    {
        coins += manager;
        UpdateUI();
    }

    void UpdateUI()
    {
        UIManager.UpdateCoins(coins);
    }
}
