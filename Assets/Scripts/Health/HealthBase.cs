using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Ebac.Core.Singleton;

public class HealthBase : MonoBehaviour
{
    public  Action OnKill;
    [SerializeField] FlashColor _flashColor;
    private int _life = 3;
    public int currentLife;
    public bool destroyOnKill;
    public bool _isDead = false;
    public float delayToDie = 1;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        currentLife = _life;
        _isDead = false;
    }

    public void Damage(int damage)
    {
        if(_isDead) return;

        currentLife -= damage;
        _flashColor.Flash();

        if(currentLife <= 0)
        {
            Kill();
        }
    }

    void Kill()
    {
        _isDead = true;

        if(destroyOnKill)
        {        
            Destroy(gameObject, delayToDie);
        }

        OnKill.Invoke();
    }
}
