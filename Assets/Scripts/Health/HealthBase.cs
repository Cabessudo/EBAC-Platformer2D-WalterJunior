using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HealthBase : MonoBehaviour
{
    public static HealthBase Instance;
    private int _life = 3;
    public int currentLife;
    public bool destroyOnKill;
    public bool _isDead = false;
    public float delayToDie = 1;

    void Awake()
    {
        Init();
        Instance = this;
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
            Player.Instance.DeadAnimation();
            Destroy(gameObject, delayToDie);
        }
    }
}
