using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour
{

    [Header("Damage")]
    public int damage = 1;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if(collision.gameObject.CompareTag("Player"))
        {
            var health = collision.gameObject.GetComponent<HealthBase>();

            if(health != null)
            {
                health.Damage(damage);
            }
        }
    }
}
