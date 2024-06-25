using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossBase : MonoBehaviour
{
    private Vector3 defaultPos;
    public BossAnim anim;
    public PlayerCheck check;
    public HealthBase bossHealth;
    public GameObject sideLimits;
    public float speed;
    public float timeStunned = 3;

    [Header("Attack")]
    public bool isAttacking;
    public float timeToAtk = 1;
    public int attacksIndex;

    [Header("Boss Shoot")]
    public EnemyGunBase bossShoot;
    public int amountShoots;
    public float timePerShoot;

    [Header("Boss Slam")]
    public List<Transform> slamPos; 
    public bool _canSlam;
    public float slamSpeed;
    public float timeToSlam;    

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        defaultPos = transform.position;
        bossHealth.OnKill += OnDeath;
    }

    void Update()
    {
        Slam();

        if(check.player)
            EnableAndDisableWalls(true);

        if(check.player && !isAttacking)
        {
            Attack();
        }
    }
    

    [NaughtyAttributes.Button]
    public void Attack()
    {
        isAttacking = true;
        attacksIndex++;

        switch(attacksIndex)
        {
            case 1:
            ShootAttack();
            break;
            
            case 2:
            SlamAttack();
            break;

            default:
            attacksIndex = 0;
            break;
        }
        
    }

    [NaughtyAttributes.Button]
    void ShootAttack()
    {
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        int shoots = 0;

        while(amountShoots > shoots)
        {
            shoots++;
            bossShoot.Shoot(ShootAnim);
            yield return new WaitForSeconds(timePerShoot);
        }

        yield return new WaitForEndOfFrame();
        anim.GetAnimByType(BossAnimType.Idle);
        isAttacking = false;
    } 

    void ShootAnim()
    {
        anim.GetAnimByType(BossAnimType.Shoot);
    }

    [NaughtyAttributes.Button]
    void SlamAttack()
    {
        StartCoroutine(SlamRoutine());
    }

    IEnumerator SlamRoutine()
    {
        anim.GetAnimByType(BossAnimType.Slam);
        Jump();
        yield return new WaitForSeconds(timeToSlam);
        RandomSlamPos();
        yield return new WaitForSeconds(timeToAtk);
        _canSlam = true;

    }

    void Jump()
    {
        transform.DOMoveY(45, 1).SetEase(Ease.Linear).SetDelay(.2f);
    }

    void RandomSlamPos()
    {
        var slamIndex = UnityEngine.Random.Range(0, slamPos.Count);
        var randomPos = new Vector2(slamPos[slamIndex].position.x, transform.position.y);
        transform.position = Vector3.Lerp(transform.position, randomPos, slamSpeed * Time.deltaTime);
    }

    void Slam()
    {
        if(_canSlam)
        {
            transform.Translate(Vector3.down * slamSpeed * Time.deltaTime);
        }
    }


    [NaughtyAttributes.Button]
    void Stunned()
    {
        //Back to the defaultPos to start stunned anim
        
        // transform.DOMove(pos, speed).SetEase(Ease.Linear).OnComplete(
        //     delegate{ StartCoroutine(StunnedRoutine()); });
        
    }

    IEnumerator StunnedRoutine()
    {
        yield return new WaitForSeconds(1);
        anim.GetAnimByType(BossAnimType.Idle);
        var pos = new Vector2(defaultPos.x, transform.position.y);
        while(Vector2.Distance(transform.position, pos) > 0.1f)
        {
            transform.position = Vector2.Lerp(transform.position, pos, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        anim.GetAnimByType(BossAnimType.Stunned);
        yield return new WaitForSeconds(timeStunned);
        anim.GetAnimByType(BossAnimType.Idle);
        isAttacking = false; //Back To Attack
    }

    void HalfLife()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Stop Slam and Start Stunned
        if(other.gameObject.CompareTag("Ground") && _canSlam)
        {
            anim.GetAnimByType(BossAnimType.Slam);
            _canSlam = false;
            StartCoroutine(StunnedRoutine());
            // if(isAttacking) Stunned();

        }
    }

    void IdleAnim()
    {
        
    }    

    void EnableAndDisableWalls(bool b)
    {
        sideLimits.SetActive(b);
    }

    void OnDeath()
    {
        EnableAndDisableWalls(false);
        bossHealth.OnKill -= OnDeath;
    }
}
