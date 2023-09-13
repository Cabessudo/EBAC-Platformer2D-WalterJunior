using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Video;
// using Unity.VisualScripting;
using UnityEditor.Callbacks;
using Ebac.Core.Singleton;

public class Player : MonoBehaviour
{

    [Header("References")]
    private Rigidbody2D _rb;
    public HealthBase playerHealth;
    public BoxCollider2D collisor;
    public JumpStyle currentJump;

    [Header("Movement")]
    private Vector2 velocity;
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    private float _flatSpeed;
    public float speedRun;
    public float jumpForce;

    [Header("Animation")]
    public Animator anim;
    //Jump Up
    public float jumpDuration;
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.5f;
    public Ease easeOut = Ease.OutBack;
    private string triggerJump = "JumpUp";
    //Jump Down
    private string triggerFall = "JumpDown";
    //Jump Landing
    private string triggerLanding = "JumpLanding";
    public float timeToLand = 1;
    //Fall
    private bool isFalling;
    public bool grounded;
    public float fallDuration;
    public float fallX;
    public float fallY;
    public float timeToFalling = 1;
    //Movement
    public float durationToSwipe = .1f;
    public string triggerToWalk = "Walk";
    public string triggerToRun = "Run";
    public bool direction;

    [Header("Live & Death")]
    public bool gameOver;
    public Image[] hearts;
    //Animation Death
    public string triggerDeath = "Death";
    
    public enum JumpStyle
    {
        Up,
        Fall,
        Land
    }

    void Awake()
    {
        if(playerHealth != null)
        {
            playerHealth.OnKill += OnPlayerDeath;
        }
    }

    void OnPlayerDeath()
    {
        playerHealth.OnKill -= OnPlayerDeath;
        DeadAnimation();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _flatSpeed = speed;
        direction = true;
    }

    // Update is called once per frame
    void Update()
    {
        HeartUI();

        if(playerHealth._isDead)
        {
            StopAllCoroutines();
            gameOver = true;
        }

        if(!gameOver)
        {          
          Jump();
          Movement();
          Falling();
        } 
            
    }

    void Movement()
    {
        if(Input.GetKey(KeyCode.D))
        {
            _rb.velocity = new Vector2(speed, _rb.velocity.y);
            direction = true;
            anim.SetBool(triggerToWalk, true);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            _rb.velocity = new Vector2(-speed, _rb.velocity.y);
            direction = false;
            anim.SetBool(triggerToWalk, true);
        }
        else
        {
            anim.SetBool(triggerToWalk, false);
            anim.SetBool(triggerToRun, false);
        }

        //Change Direction
        if(direction)
        _rb.transform.DOScaleX(1, durationToSwipe);
        else
        _rb.transform.DOScaleX(-1, durationToSwipe);
        
        //Run
        if(Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool(triggerToRun, true);
            speed = speedRun;
        }
        else
        {
            anim.SetBool(triggerToRun, false);
            speed = _flatSpeed;
        }

        //Friction
        if(_rb.velocity.x > 0)
        {
            _rb.velocity -= friction;            
        }
        else if(_rb.velocity.x < 0)
        {
            _rb.velocity += friction;
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && grounded && !gameOver)
        {
            _rb.velocity = Vector2.up * jumpForce;
            grounded = false;

            if(direction)
            _rb.transform.localScale = Vector2.one;
            else
            _rb.transform.localScale = new Vector2(-1, 1);

            _rb.transform.DOKill();
            
            JumpAnimation();
            StartCoroutine(FallingAnimantion());
        }
    }

    void JumpAnimation()
    {
        SwitchJumpStyle(JumpStyle.Up);
        if(direction)
        {
            _rb.transform.DOScaleX(jumpScaleX, jumpDuration/2).SetEase(easeOut).OnComplete(
            delegate
            {
                _rb.transform.DOScaleX(1, jumpDuration/2).SetEase(easeOut);
            });
        }    
        else
        {
            _rb.transform.DOScaleX(-jumpScaleX, jumpDuration/2).SetEase(easeOut).OnComplete(
            delegate
            {
                _rb.transform.DOScaleX(-1, jumpDuration/2).SetEase(easeOut);
            });
        }

        _rb.transform.DOScaleY(jumpScaleY, jumpDuration/2).SetEase(easeOut).OnComplete(
        delegate
        {
            _rb.transform.DOScaleY(1, jumpDuration/2).SetEase(easeOut);
        });
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") && !grounded && !gameOver)
        {
            Land();
            grounded = true;
            isFalling = false;
        }
    }

    void Land()
    {
        SwitchJumpStyle(JumpStyle.Land);

        _rb.transform.DOKill();

        if(direction)
        {
            _rb.transform.DOScaleX(fallX, fallDuration/2).SetEase(easeOut).OnComplete(
            delegate
            {
                _rb.transform.DOScaleX(1, fallDuration/2).SetEase(easeOut);
            });
        }

        if(!direction)
        {
            _rb.transform.DOScaleX(-fallX, fallDuration/2).SetEase(easeOut).OnComplete(
                delegate
                {
                    _rb.transform.DOScaleX(-1, fallDuration/2).SetEase(easeOut);
                });
        }

        _rb.transform.DOScaleY(fallY, fallDuration/2).SetEase(easeOut).OnComplete(
            delegate
            {
                _rb.transform.DOScaleY(1, fallDuration/2).SetEase(easeOut);
            });
    }

    void Falling()
    {
        if(!grounded && isFalling && !gameOver)
        {
            SwitchJumpStyle(JumpStyle.Fall);
        }
    }
    IEnumerator FallingAnimantion()
    {
        yield return new WaitForSeconds(timeToFalling);
        isFalling = true;
    }

    IEnumerator LandAnimation()
    {
        yield return new WaitForSeconds(timeToLand);
        anim.SetBool(triggerLanding, false);
    }

    void HeartUI()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < playerHealth.currentLife)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    void SwitchJumpStyle(JumpStyle newStyle)
    {
        currentJump = newStyle;
        
        if(newStyle == JumpStyle.Up) anim.SetTrigger(triggerJump);
        if(newStyle == JumpStyle.Fall) anim.SetTrigger(triggerFall);
        if(newStyle == JumpStyle.Land) anim.SetTrigger(triggerLanding);
    }

    public void DeadAnimation()
    {
        anim.SetTrigger(triggerDeath);
    }
}
