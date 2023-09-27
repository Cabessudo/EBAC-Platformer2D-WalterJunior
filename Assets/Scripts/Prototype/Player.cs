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
    public Animator currentPlayer;
    private Rigidbody2D _rb;
    public HealthPlayer playerHealth;
    public BoxCollider2D collisor;
    public ParticleSystem PS_dust;
    public JumpStyle currentJump;

    [Header("Player Setup")]
    public SOPlayerSetup soPlayerSetup;

    [Header("Jump Setup Check")]
    public float distToGround;
    public float spaceToGround = .1f;
    
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

        currentPlayer = Instantiate(soPlayerSetup.anim, transform);

        if(collisor != null)
        {
            distToGround = collisor.bounds.extents.y;
        }
    }

    private bool GroundCheck()
    {
        Debug.DrawLine(transform.position, Vector2.down, Color.red, distToGround + spaceToGround);
        return Physics2D.Raycast(transform.position, Vector2.down, distToGround + spaceToGround);
    }

    void OnPlayerDeath()
    {
        playerHealth.OnKill -= OnPlayerDeath;
        DeadAnimation();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AwakeAnim());
        Init();
    }

    void Init()
    {
        _rb = GetComponent<Rigidbody2D>();
        soPlayerSetup.gameOver = false;
        soPlayerSetup.direction = true;
        soPlayerSetup.isFalling = false;
        soPlayerSetup.grounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(soPlayerSetup.cutScene) 
        {
            _rb.velocity = Vector3.zero;
            currentPlayer.SetBool(soPlayerSetup.triggerToWalk, false);
            return;
        }

        if(playerHealth.soHealth._isDead)
        {
            StopAllCoroutines();
            soPlayerSetup.gameOver = true;
        }

        if(!soPlayerSetup.gameOver)
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
            _rb.velocity = new Vector2(soPlayerSetup.speed, _rb.velocity.y);
            soPlayerSetup.direction = true;
            currentPlayer.SetBool(soPlayerSetup.triggerToWalk, true);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            _rb.velocity = new Vector2(-soPlayerSetup.speed, _rb.velocity.y);
            soPlayerSetup.direction = false;
            currentPlayer.SetBool(soPlayerSetup.triggerToWalk, true);
        }
        else
        {
            currentPlayer.SetBool(soPlayerSetup.triggerToWalk, false);
            currentPlayer.SetBool(soPlayerSetup.triggerToRun, false);
        }

        //Change Direction
        if(soPlayerSetup.direction)
        _rb.transform.DOScaleX(1, soPlayerSetup.durationToSwipe);
        else
        _rb.transform.DOScaleX(-1, soPlayerSetup.durationToSwipe);
        
        //Run
        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentPlayer.SetBool(soPlayerSetup.triggerToRun, true);
            soPlayerSetup.speed = soPlayerSetup.speedRun;
        }
        else
        {
            currentPlayer.SetBool(soPlayerSetup.triggerToRun, false);
            soPlayerSetup.speed = soPlayerSetup.flatSpeed;
        }

        //Friction
        if(_rb.velocity.x > 0)
        {
            _rb.velocity -= soPlayerSetup.friction;            
        }
        else if(_rb.velocity.x < 0)
        {
            _rb.velocity += soPlayerSetup.friction;
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && soPlayerSetup.grounded && !soPlayerSetup.gameOver && GroundCheck())
        {
            VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.Jump, transform.position);
            PS_dust.Stop();
            _rb.velocity = Vector2.up * soPlayerSetup.jumpForce;
            soPlayerSetup.grounded = false;

            if(soPlayerSetup.direction)
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
        if(soPlayerSetup.direction)
        {
            _rb.transform.DOScaleX(soPlayerSetup.jumpScaleX, soPlayerSetup.jumpDuration / 2).SetEase(soPlayerSetup.easeOut).OnComplete(
            delegate
            {
                _rb.transform.DOScaleX(1, soPlayerSetup.jumpDuration / 2).SetEase(soPlayerSetup.easeOut);
            });
        }    
        else
        {
            _rb.transform.DOScaleX(-soPlayerSetup.jumpScaleX, soPlayerSetup.jumpDuration/2).SetEase(soPlayerSetup.easeOut).OnComplete(
            delegate
            {
                _rb.transform.DOScaleX(-1, soPlayerSetup.jumpDuration/2).SetEase(soPlayerSetup.easeOut);
            });
        }

        _rb.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.jumpDuration/2).SetEase(soPlayerSetup.easeOut).OnComplete(
        delegate
        {
            _rb.transform.DOScaleY(1, soPlayerSetup.jumpDuration/2).SetEase(soPlayerSetup.easeOut);
        });
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") && !soPlayerSetup.grounded && !soPlayerSetup.gameOver)
        {
            PS_dust.Play();
            Land();
            soPlayerSetup.grounded = true;
            soPlayerSetup.isFalling = false;
        }
    }

    void Land()
    {
        VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.Jump, transform.position);
        SwitchJumpStyle(JumpStyle.Land);

        _rb.transform.DOKill();

        if(soPlayerSetup.direction)
        {
            _rb.transform.DOScaleX(soPlayerSetup.fallX, soPlayerSetup.fallDuration/2).SetEase(soPlayerSetup.easeOut).OnComplete(
            delegate
            {
                _rb.transform.DOScaleX(1, soPlayerSetup.fallDuration/2).SetEase(soPlayerSetup.easeOut);
            });
        }

        if(!soPlayerSetup.direction)
        {
            _rb.transform.DOScaleX(-soPlayerSetup.fallX, soPlayerSetup.fallDuration/2).SetEase(soPlayerSetup.easeOut).OnComplete(
                delegate
                {
                    _rb.transform.DOScaleX(-1, soPlayerSetup.fallDuration/2).SetEase(soPlayerSetup.easeOut);
                });
        }

        _rb.transform.DOScaleY(soPlayerSetup.fallY, soPlayerSetup.fallDuration/2).SetEase(soPlayerSetup.easeOut).OnComplete(
            delegate
            {
                _rb.transform.DOScaleY(1, soPlayerSetup.fallDuration/2).SetEase(soPlayerSetup.easeOut);
            });
    }

    void Falling()
    {
        if(!soPlayerSetup.grounded && soPlayerSetup.isFalling && !soPlayerSetup.gameOver)
        {
            SwitchJumpStyle(JumpStyle.Fall);
        }
    }
    IEnumerator FallingAnimantion()
    {
        yield return new WaitForSeconds(soPlayerSetup.timeToFalling);
        soPlayerSetup.isFalling = true;
    }

    IEnumerator LandAnimation()
    {
        yield return new WaitForSeconds(soPlayerSetup.timeToLand);
        currentPlayer.SetBool(soPlayerSetup.triggerLanding, false);
    }

    void SwitchJumpStyle(JumpStyle newStyle)
    {
        currentJump = newStyle;
        
        if(newStyle == JumpStyle.Up) currentPlayer.SetTrigger(soPlayerSetup.triggerJump);
        if(newStyle == JumpStyle.Fall) currentPlayer.SetTrigger(soPlayerSetup.triggerFall);
        if(newStyle == JumpStyle.Land) currentPlayer.SetTrigger(soPlayerSetup.triggerLanding);
    }

    IEnumerator AwakeAnim()
    {
        soPlayerSetup.cutScene = true;
        currentPlayer.SetTrigger(soPlayerSetup.triggerToAwake);
        yield return new WaitForSeconds(soPlayerSetup.cutSceneDuration);
        soPlayerSetup.cutScene = false;
    }


    public void DeadAnimation()
    {
        currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
    }
}
