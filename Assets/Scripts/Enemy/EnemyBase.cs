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
    [SerializeField] EnemyAnim _anim;
    [SerializeField] List<Transform> waypoints;
    protected int waypointIndex;

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

        StartCoroutine(PatrolRoutine());

    }

    void OnEnemyDeath()
    {
        enemyHealth.OnKill -= OnEnemyDeath;
        DeadAnimation();
    }

    public virtual void Patrol()
    {
        StopAllCoroutines();
        StartCoroutine(PatrolRoutine());
    }

    IEnumerator PatrolRoutine()
    {
        // _anim.GetAnimByType(EnemyAnimType.Run);
        while(Vector3.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        waypointIndex++;
        if(waypointIndex > waypoints.Count)
        waypointIndex = 0;
        
        StopAllCoroutines();
        direction *= -1; 
        transform.localScale = new Vector3(direction, 1, 1);
        StartCoroutine(PatrolRoutine());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if(collision.gameObject.CompareTag("Player"))
        {
            _anim.GetAnimByType(EnemyAnimType.Attack);
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
        _anim.GetAnimByType(EnemyAnimType.Death);
        enemyCollider.enabled = false;
    }

    public void TakeDamage(int amount)
    {
        enemyHealth.Damage(amount);
        enemyAudio.PlayOneShot(damageAudio, 1);
        enemyHealth.flashColor.Flash();
    } 
}
