using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Ebac.Core.Singleton;

public class HealthBase : MonoBehaviour, IDamageable
{
    public Action OnKill; // IMPORTANT TO REMEMBER
    public FlashColor flashColor;
    [Header("Health Setup")]
    public SO_Health soHealth;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        soHealth.currentLife = soHealth._life;
        soHealth._isDead = false;
    }

    void Start()
    {
        flashColor = GetComponentInChildren<FlashColor>();
    }

    public virtual void Damage(int damage)
    {
        if(soHealth._isDead) return;

        soHealth.currentLife -= damage;
        flashColor?.Flash();

        if(soHealth.currentLife <= 0)
        {
            Kill();
        }
    }

    public virtual void Kill()
    {
        soHealth._isDead = true;

        if(soHealth.destroyOnKill)
        {        
            Destroy(gameObject, soHealth.delayToDie);
        }

        OnKill.Invoke();
    }
}
