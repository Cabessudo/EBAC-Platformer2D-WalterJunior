using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossBase : MonoBehaviour
{
    private Vector3 defaultPos;
    public List<Transform> slamPos; 
    public BossAnim anim;
    public float speed;
    public float timeToAtk = 1;

    [Header("Boss Shoot")]
    public BossShoot bossShoot;
    public int amountShoots;
    public int timePerShoot;
    public int attacksIndex;

    [Header("Boss Slam")]
    private bool _canSlam;
    public float timeToSlam;
    public float timeToBack = 1;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.position;
    }

    void Update()
    {
        Slam();
    }
    
    public void Attack()
    {
        attacksIndex++;

        switch(attacksIndex)
        {
            case 1:
            ShootAttack();
            break;
            
            case 2:
            SlamAttack();
            break;

            case 3:
            Stunned();
            break;

            default:
            attacksIndex = 1;
            break;
        }
        
    }

    void ShootAttack()
    {
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        while(amountShoots > 0)
        {
            amountShoots++;
            bossShoot.Shoot();
            yield return new WaitForSeconds(1);
        }

        yield return new WaitForEndOfFrame();
    } 

    void SlamAttack()
    {
        anim.GetAnimByType(BossAnimType.Jump);
        StartCoroutine(SlamRoutine());
    }

    IEnumerator SlamRoutine()
    {
        yield return new WaitForSeconds(timeToAtk);
        int randomPos = UnityEngine.Random.Range(0, slamPos.Count);
        transform.position = slamPos[randomPos].position;
        yield return new WaitForSeconds(timeToSlam);
        anim.GetAnimByType(BossAnimType.Slam);
        _canSlam = true;
        yield return new WaitForSeconds(timeToBack);
        transform.position = Vector3.Lerp(transform.position, defaultPos, speed * Time.deltaTime);
        yield return new WaitForEndOfFrame();

    }

    void Slam()
    {
        if(_canSlam)
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void Stunned()
    {
        anim.GetAnimByType(BossAnimType.Stunned);
    }

    void HalfLife()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
            _canSlam = false;
    }
}
