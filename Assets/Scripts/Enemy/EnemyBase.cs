using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour
{

    [Header("References")]
    public BoxCollider2D enemyCollider;
    public HealthBase enemyHealth;
    public SO_Health soEnemyHealth;
    [SerializeField] Animator _anim;

    [Header("Damage")]
    public int damage = 1;
    public string triggerAttack = "Attack";
    public string triggerDead = "Death";

    void Awake()
    {
        if(enemyHealth != null)
        {
            enemyHealth.OnKill += OnEnemyDeath;
        }
    }

    void OnEnemyDeath()
    {
        enemyHealth.OnKill -= OnEnemyDeath;
        DeadAnimation();
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

                if(health.soHealth._isDead)
                enemyHealth.flashColor.Death();
            }
        }
    }

    public void DeadAnimation()
    {
        _anim.SetTrigger(triggerDead);
        enemyCollider.enabled = false;
    }

    public void TakeDamage(int amount)
    {
        enemyHealth.Damage(amount);
        enemyHealth.flashColor.Flash();
    } 
}
