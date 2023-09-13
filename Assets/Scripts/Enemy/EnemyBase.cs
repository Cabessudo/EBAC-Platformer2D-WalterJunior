using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour
{

    [Header("References")]
    public HealthBase enemyHealth;
    public FlashColor enemyFlash;
    [SerializeField] Animator _anim;

    [Header("Damage")]
    public int damage = 1;
    public string triggerAttack = "Attack";
    public string triggerDead = "Death";

    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if(collision.gameObject.CompareTag("Player"))
        {
            _anim.SetTrigger(triggerAttack);
            var health = collision.gameObject.GetComponent<HealthBase>();

            if(health != null)
            {
                health.Damage(damage);
            }
        }
    }

    public void DeadAnimation()
    {
        _anim.SetTrigger(triggerDead);
    }

    public void TakeDamage(int amount)
    {
        enemyHealth.Damage(amount);
        enemyFlash.Flash();
    } 
}
