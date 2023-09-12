using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public static ItemManager Instance; 
    public int coins;

    void Awake()
    {
        if(Instance == null)
        Instance = this;
        else
        Destroy(gameObject);
    }

    void Start()
    {
        Restart();
    }

    void Restart()
    {
        coins = 0;
    }

    public void AddCoins(int manager = 1)
    {
        coins += manager;
    }
}
