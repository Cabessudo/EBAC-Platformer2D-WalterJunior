using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : EnemyBase
{
    [Header("Shooter")]
    public EnemyGunBase enemyGun;
    public PlayerCheck check;
    public bool isAttacking;

    [Header("Shoot Parameters")]
    public float timePerShoot; 

    void Update()
    {
        LookAtPlayer();
        
        if(check.player && !isAttacking)
        {
            Shoot();
        }
        
        if(!check.player && isAttacking)
        {
            isAttacking = false;
            Patrol();
        }
    }

    void Shoot()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
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

    void LookAtPlayer()
    {
        if(check.player)
        {
            var playerPos = Player.Instance.transform.position;
            var playerX = playerPos.x; 

            if(playerX > transform.position.x)
            { 
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(playerX < transform.position.x)
            {
                transform.localScale = Vector3.one;
            }
        }
    }
}
