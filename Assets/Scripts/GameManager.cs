using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class GameManager : Singleton<GameManager>
{
    void Awake()
    {
        if(Instance == null)
           Instance = this;
        else
           Destroy(gameObject);
    }
}
