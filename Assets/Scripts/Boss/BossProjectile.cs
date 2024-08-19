using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : ProjectileBase
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && !hitChance)
        {
            hitChance = true;
            var damageable = other.transform.GetComponent<IDamageable>();
            damageable?.Damage(projectileDamage);
            
            Destroy(gameObject);
        }
        
    }
}
