using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class ItemManager : Singleton<ItemManager>
{ 
    public SOInt coins;
    public SOUIIntUpdate SOUIIntUpdate;

    void Start()
    {
        Restart();
    }

    void Restart()
    {
        coins.value = 0;
        UpdateUI();
    }

    public void AddCoins(int manager = 1)
    {
        coins.value += manager;
        UpdateUI();
    }

    void UpdateUI()
    {
        SOUIIntUpdate.UpdateValue();
    }
}
