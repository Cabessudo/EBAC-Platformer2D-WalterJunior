using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public Action OnKill; // IMPORTANT TO REMEMBER
    public Action OnDamage;
    public Collider2D objCollider;
    public FlashColor flashColor;
    public AudioSource damageSound;
    
    [Header("Health Setup")]
    public SO_Health soHealth;
    public int currLife;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        currLife = soHealth.maxLife;
        soHealth._isDead = false;
        soHealth.canHit = true;
    }

    public virtual void Start()
    {
        flashColor = GetComponentInChildren<FlashColor>();
    }
    
    [NaughtyAttributes.Button]
    public void DamageButton()
    {
        Damage();
    }   

    public virtual void Damage(int damage = 1)
    {
        if(soHealth._isDead) return;

        currLife -= damage;
        flashColor?.Flash();
        damageSound?.Play();


        if(currLife <= 0)
        {
            Kill();
        }
    }

    public virtual void Kill()
    {
        OnKill?.Invoke();
        soHealth._isDead = true;
        if(objCollider != null) objCollider.enabled = false;

        if(soHealth.destroyOnKill)
        {        
            Destroy(gameObject, soHealth.delayToDie);    
        }

    }

    public void DisableAllSprites()
    {
        flashColor?.DisableAllSprites();
    }

    void OnDestroy()
    {
        DisableAllSprites();
    }
}
