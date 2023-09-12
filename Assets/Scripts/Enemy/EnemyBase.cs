using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour
{

    [Header("References")]
    // public Rigidbody2D rb;
    // public Ease ease;
    [SerializeField] Animator _anim;

    [Header("Damage")]
    public int damage = 1;
    public string triggerAttack = "Attack";

    void Start()
    {
        //rb.transform.DOMoveX(-3, 1).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
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
}
