using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Vector2 projectDirection;
    public float speed = 1;
    public float timeToDestroy = 1;
    public int projectileDamage = 1;
    protected bool hitChance = false;

    void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        transform.Translate(projectDirection * Time.deltaTime * speed);
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy") && !hitChance)
        {
            hitChance = true;
            var enemyHealth = other.gameObject.GetComponent<HealthBase>();

            if(enemyHealth != null && !enemyHealth.soHealth._isDead)
            {
                enemyHealth.Damage(projectileDamage);
                gameObject.SetActive(false);
            }
        }
    }
}
