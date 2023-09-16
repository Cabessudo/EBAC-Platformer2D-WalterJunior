using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Vector3 projectDirection;
    public float side = 1;
    public float timeToDestroy = 1;
    public int projectileDamage = 1;

    void Awake()
    {
        Destroy(gameObject, 1);
    }

    void Update()
    {
        transform.Translate(projectDirection * Time.deltaTime * side);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.gameObject.GetComponent<EnemyBase>();
        var enemyHealth = other.gameObject.GetComponent<HealthBase>();

        if(enemy != null && !enemyHealth.soHealth._isDead)
        {
            enemy.TakeDamage(projectileDamage);
            gameObject.SetActive(false);
        }
    }
}
