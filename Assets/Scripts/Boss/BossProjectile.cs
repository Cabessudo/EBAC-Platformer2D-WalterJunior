using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public int damage = 1;
    public float speed;
    
    void Start()
    {
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var damageable = collision.transform.GetComponent<IDamageable>();
        damageable?.Damage(damage);
    }
    
}