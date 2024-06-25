using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour
{

    [Header("References")]
    public BoxCollider2D enemyCollider;
    public HealthBase enemyHealth;
    public AudioClip damageAudio;
    public AudioSource enemyAudio;
    public EnemyAnim anim;
    [SerializeField] List<Transform> waypoints;
    public int waypointIndex;

    [Header("Parameters")]
    protected int direction = 1;
    public int damage = 1;
    public float speed = 5;

    void Start()
    {
        if(enemyHealth != null)
        {
            enemyHealth.OnKill += OnEnemyDeath;
        }

        Patrol();
    }

    void OnEnemyDeath()
    {
        enemyHealth.OnKill -= OnEnemyDeath;
        DeadAnimation();
        StopAllCoroutines();
    }

    public virtual void Patrol()
    {
        StopAllCoroutines();
        StartCoroutine(PatrolRoutine());
    }

    IEnumerator PatrolRoutine()
    {
        
        var waypointPos = new Vector2(waypoints[waypointIndex].position.x, transform.position.y);
        while(Vector3.Distance(transform.position, waypoints[waypointIndex].position) > 0.1f)
        {
            LookAtWaypoint();
            transform.position = Vector3.MoveTowards(transform.position, waypointPos, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);
        ChangeDirection();
    }

    void ChangeDirection()
    {
        StopAllCoroutines();
        waypointIndex++;
        if(waypointIndex > waypoints.Count - 1) waypointIndex = 0;
        StartCoroutine(PatrolRoutine());
    }

    void LookAtWaypoint()
    {
        transform.localScale = new Vector3(direction, 1, 1);
        if(waypoints[waypointIndex].position.x > transform.position.x)
            direction = -1;

        if(waypoints[waypointIndex].position.x < transform.position.x)
            direction = 1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if(collision.gameObject.CompareTag("Player"))
        {
            anim.GetAnimByType(EnemyAnimType.Attack);
            var health = collision.gameObject.GetComponent<HealthBase>();

            if(health != null)
            {
                health.Damage(damage);

                if(health.soHealth._isDead)
                enemyHealth.flashColor.Death();
            }
        }
    }

    public void DeadAnimation()
    {
        anim.GetAnimByType(EnemyAnimType.Death);
        enemyCollider.enabled = false;
    }

    public void TakeDamage(int amount)
    {
        enemyHealth.Damage(amount);
        enemyAudio.PlayOneShot(damageAudio, 1);
        enemyHealth.flashColor.Flash();
    } 
}
