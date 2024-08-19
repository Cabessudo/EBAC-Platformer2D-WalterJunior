using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour
{

    [Header("References")]
    public BoxCollider2D enemyCollider;
    public HealthBase enemyHealth;
    public PlayerCheck check;
    public AudioSource enemyAudio;
    public EnemyAnim anim;
    [SerializeField] List<Transform> waypoints;

    [Header("Parameters")]
    //Movement
    public int waypointIndex;
    protected int direction = 1;
    protected float waitToPatrol = 1;
    public float speed = 5;
    
    //Attack
    public bool isAttacking;
    public int damage = 1;

    public virtual void Start()
    {
        Init();

        if(enemyHealth != null)
        {
            enemyHealth.OnKill += OnEnemyDeath;
            enemyHealth.OnDamage += OnEnemyDamage;
        }
    }

    void Init()
    {
        Patrol();
    }

    void Update()
    {
        if(!enemyHealth.soHealth._isDead)
        {
            LookAtPlayer();
            
            if(check.player && !isAttacking)
            {
                Attack();
            }
            
            if(!check.player && isAttacking)
            {
                isAttacking = false;
                Patrol();
            }
        }
    }

    public virtual void Attack()
    {}

    
    public virtual void OnEnemyDeath()
    {
        DeadAnimation();
        StopAllCoroutines();
        enemyHealth.OnKill -= OnEnemyDeath;
        enemyHealth.OnDamage -= OnEnemyDamage;
    }

    public void Patrol()
    {
        StopAllCoroutines();
        StartCoroutine(PatrolRoutine());
    }

    IEnumerator PatrolRoutine()
    {
        yield return new WaitForSeconds(waitToPatrol);
        var waypointPos = new Vector2(waypoints[waypointIndex].position.x, transform.position.y);

        while(Vector2.Distance(transform.position, waypointPos) > 0.5f)
        {
            LookAtWaypoint();
            transform.position = Vector2.MoveTowards(transform.position, waypointPos, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);
        ChangeDirection();
    }

    public void ChangeDirection()
    {
        StopAllCoroutines();
        waypointIndex++;
        if(waypointIndex > waypoints.Count - 1) waypointIndex = 0;
        StartCoroutine(PatrolRoutine());
    }

    void LookAtWaypoint()
    {
        //Update the direction to look if...
        transform.localScale = new Vector3(direction, 1, 1);

        //The current waypointIndex is in its right
        if(waypoints[waypointIndex].position.x > transform.position.x)
            direction = -1;

        //Or in its left
        if(waypoints[waypointIndex].position.x < transform.position.x)
            direction = 1;
    }

    void LookAtPlayer()
    {
        if(check.player && check.playerPos != null)
        {
            var playerX = check.playerPos.position.x; 

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

    void OnTriggerStay2D(Collider2D other)
    {

        if(other.gameObject.CompareTag("Player"))
        {
            var playerHealth = other.gameObject.GetComponent<HealthBase>();

            if(playerHealth != null)
            {
                if(playerHealth.soHealth.canHit)
                {
                    playerHealth.Damage(damage);
                    anim.GetAnimByType(EnemyAnimType.Attack);
                }

                if(playerHealth.soHealth._isDead)
                enemyHealth.flashColor.Death();
            }
        }
    }

    public void DeadAnimation()
    {
        anim.GetAnimByType(EnemyAnimType.Death);
        enemyCollider.enabled = false;
    }

    public void OnEnemyDamage()
    {
        enemyAudio.Play();
        enemyHealth.flashColor.Flash();
    } 

    void OnDestroy()
    {
        transform.DOKill();
    }
}
