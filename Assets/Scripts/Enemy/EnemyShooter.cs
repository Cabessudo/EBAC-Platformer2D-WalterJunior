using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : EnemyBase
{
    [Header("Shooter")]
    public EnemyGunBase enemyGun;
    

    [Header("Shoot Parameters")]
    public float timeToShoot;
    public float timePerShoot; 

    public override void Attack()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        yield return new WaitForSeconds(timeToShoot);

        while(true)
        {
            enemyGun.Shoot(ShootAnim);
            yield return new WaitForSeconds(timePerShoot);
        }
    }

    void ShootAnim()
    {
        anim.GetAnimByType(EnemyAnimType.Attack);
    }

    
}
