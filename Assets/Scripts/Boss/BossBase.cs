using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossBase : MonoBehaviour
{
    private Vector3 defaultPos;
    public BossAnim anim;
    public PlayerCheck check;
    public GameObject sideLimits;
    public BossShadowAnim bossShadow;
    public AudioSource bossSound;
    public float speed;
    public float timeStunned = 3;
    private bool _onceCheckPlayer;

    [Header("Boss Health")]
    public HealthBase bossHealth;
    public GameObject bossHealthUI;
    public bool _halfLifeCheck;

    [Header("Attack")]
    public bool isAttacking;
    public float timeToAtk = 1;
    public int attacksIndex;

    [Header("Boss Shoot")]
    public EnemyGunBase bossShoot;
    public int amountShoots;
    public float timePerShoot;

    [Header("Boss Slam")]
    public RandomPos slamPos; 
    public bool _canSlam;
    public float slamSpeed;
    public float timeToSlam;    

    [Header("Boss Angry")]
    public BossCutscene bossCutscene;
    public Color angryColor;
    private bool _canBeAngry;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        slamPos.speed = speed;
        defaultPos = transform.position;
        bossHealth.OnKill += OnDeath;
    }

    void Update()
    {
        if(!bossHealth.soHealth._isDead)
        {
            PlayerEnterRangeCheck();
            Slam();
            CheckHalfLife(); 

            if(check.player && !bossCutscene.cutsceneOn)
            {
                if(!isAttacking)
                {
                    Attack();
                }
            }

            //When player die stop attacks
            if(Player.Instance.playerHealth.soHealth._isDead && _onceCheckPlayer && !isAttacking)
            {
                _onceCheckPlayer = false;
                StopAllCoroutines();
                anim.GetAnimByType(BossAnimType.Idle);
            }
        }
    }

    #region  ShootAttack
    [NaughtyAttributes.Button]
    public void Attack()
    {
        isAttacking = true;
        attacksIndex++;

        if(attacksIndex > 2)
            attacksIndex = 1;

        switch(attacksIndex)
        {
            case 1:
            ShootAttack();
            break;
            
            case 2:
            SlamAttack();
            break;
        }
        
    }

    [NaughtyAttributes.Button]
    void ShootAttack()
    {
        StopAllCoroutines();
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        _canBeAngry = true;
        yield return new WaitForSeconds(timeToAtk);
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
    #endregion

    #region SlamAttack

    [NaughtyAttributes.Button]
    void SlamAttack()
    {
        StopAllCoroutines();
        StartCoroutine(SlamRoutine());
    }

    IEnumerator SlamRoutine()
    {
        _canBeAngry = false;
        bossHealth.soHealth.canHit = false;
        yield return new WaitForSeconds(timeToAtk);
        anim.GetAnimByType(BossAnimType.Slam); //The anim of slam has a anim looking like is jumping too
        bossShadow?.Fade(); //Make the shadow disappear
        Jump();
        yield return new WaitForSeconds(timeToSlam);
        slamPos.RandomPosition();
        yield return new WaitForSeconds(timeToSlam);
        slamPos.Stop();
        bossShadow?.Appear(); //Make the shadow disappear
        _canSlam = true;
    }

    void Jump()
    {
        transform.DOMoveY(45, .5f).SetEase(Ease.Linear).SetDelay(.2f);
    }

    void Slam()
    {
        if(_canSlam)
        {
            transform.Translate(Vector3.down * slamSpeed * Time.deltaTime);
        }
    }
    #endregion

    #region Stun
    [NaughtyAttributes.Button]
    void Stunned()
    {
        StartCoroutine(StunnedRoutine());
    }

    IEnumerator StunnedRoutine()
    {
        yield return new WaitForSeconds(.5f);
        anim.GetAnimByType(BossAnimType.Idle);
        var pos = new Vector3(defaultPos.x, transform.position.y, transform.position.z);
        while(Vector3.Distance(transform.position, pos) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        anim.GetAnimByType(BossAnimType.Stunned);
        _canBeAngry = true;
        yield return new WaitForSeconds(timeStunned);
        anim.GetAnimByType(BossAnimType.Idle);
        isAttacking = false; //Back To Attack
    }
    #endregion

    #region  Life
    void CheckHalfLife()
    {
        if(bossHealth.currLife <= bossHealth.soHealth.maxLife / 2 && !_halfLifeCheck)
        {
            bossHealth.soHealth.canHit = false;

            if(_canBeAngry)
            {
                StopAllCoroutines();
                isAttacking = false;
                anim.GetAnimByType(BossAnimType.Idle);
            }
            

            if(!isAttacking)
            {
                bossHealth.soHealth.canHit = true;
                _halfLifeCheck = true;
                bossCutscene.Cutscene();
                bossHealth.flashColor.ChangeColor(angryColor);
                anim.anim.speed = 1.5f;
                amountShoots = 10;
                timeStunned = 2;
                timeToAtk /= 2;
                slamSpeed += 50;
                slamPos.speed = speed;
            }
        }
    }

    void OnDeath()
    {
        StopAllCoroutines();
        anim.GetAnimByType(BossAnimType.Death);
        EnableAndDisableBossBattle(false);
        if(!_canSlam) isAttacking = false;
        bossHealth.OnKill -= OnDeath;
    }
    #endregion    

    #region Utils

    void EnableAndDisableBossBattle(bool b)
    {
        sideLimits.SetActive(b);
        bossHealthUI.SetActive(b);
    }

    void PlayerEnterRangeCheck()
    {
        if(check.player && !bossHealth.soHealth._isDead && !_onceCheckPlayer)
        {
            _onceCheckPlayer = true;
            EnableAndDisableBossBattle(true);
            bossCutscene.Cutscene();
        }
       
    }

    #endregion

    void OnTriggerEnter2D(Collider2D other)
    {
        //Stop Slam and Start Stunned
        if(other.gameObject.CompareTag("Ground") && _canSlam)
        {
            anim.GetAnimByType(BossAnimType.Slam);
            _canSlam = false;
            bossHealth.soHealth.canHit = true;
            
            if(isAttacking) Stunned();

        }

        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            var playerHealth = other.gameObject.GetComponent<HealthBase>();
            playerHealth?.Damage();
        }
    }
}