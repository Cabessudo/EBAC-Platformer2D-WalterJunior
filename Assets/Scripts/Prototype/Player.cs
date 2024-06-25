using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Video;
// using Unity.VisualScripting;
using UnityEditor.Callbacks;
using Ebac.Core.Singleton;

public class Player : Singleton<Player>
{

    [Header("References")]
    public PlayerAnim playerAnim;
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

    // Start is called before the first frame update
    void Start()
    {
        Init();
        SOInit();
        AwakeAnim();
    }

    void Init()
    {
        _rb = GetComponent<Rigidbody2D>();

        //Set the action deatg
        if(playerHealth != null)
        {
            playerHealth.OnKill += OnPlayerDeath;
        }

        //Spawn the player and set the animator in the AnimPlayer Script
        playerAnim.anim = Instantiate(soPlayerSetup.anim, transform);

        //Set the distance to check if player hits the ground with the height of the box collider y
        if(collisor != null)
        {
            distToGround = collisor.bounds.extents.y;
        }
    }

    void SOInit()
    {
        soPlayerSetup.gameOver = false;
        soPlayerSetup.direction = true;
        soPlayerSetup.isFalling = false;
        soPlayerSetup.grounded = true;
        soPlayerSetup.readyToJump = true;
        soPlayerSetup.isWalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(soPlayerSetup.cutscene) 
        {
            _rb.velocity = Vector3.zero;
            playerAnim.GetAnimByType(PlayerAnimType.Walk, false);
            playerAnim.GetAnimByType(PlayerAnimType.Run, false);
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

    public void Cutscene()
    {
        soPlayerSetup.cutscene = true;
    }

    void OnPlayerDeath()
    {
        playerHealth.OnKill -= OnPlayerDeath;
        DeadAnimation();
    }

    #region  Movement
    void Movement()
    {
        if(Input.GetKey(KeyCode.D))
        {
            _rb.velocity = new Vector2(soPlayerSetup.speed, _rb.velocity.y);
            soPlayerSetup.direction = true;
            soPlayerSetup.isWalking = true;
            playerAnim.GetAnimByType(PlayerAnimType.Walk, true);

        }
        else if(Input.GetKey(KeyCode.A))
        {
            _rb.velocity = new Vector2(-soPlayerSetup.speed, _rb.velocity.y);
            soPlayerSetup.direction = false;
            soPlayerSetup.isWalking = true;
            playerAnim.GetAnimByType(PlayerAnimType.Walk, true);
        }
        else
        {
            playerAnim.GetAnimByType(PlayerAnimType.Walk, false);
            playerAnim.GetAnimByType(PlayerAnimType.Run, false);
            soPlayerSetup.isWalking = false;
        }

        //Change Direction
        if(soPlayerSetup.direction)
        _rb.transform.DOScaleX(1, soPlayerSetup.durationToSwipe);
        else
        _rb.transform.DOScaleX(-1, soPlayerSetup.durationToSwipe);
        
        //Run
        if(Input.GetKey(KeyCode.LeftShift) && soPlayerSetup.isWalking)
        {
            playerAnim.GetAnimByType(PlayerAnimType.Run, true);
            soPlayerSetup.speed = soPlayerSetup.speedRun;
        }
        else 
        {
            playerAnim.GetAnimByType(PlayerAnimType.Run, false);
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

    #endregion

    #region  Jump
    
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && soPlayerSetup.grounded && !soPlayerSetup.gameOver && GroundCheck() && soPlayerSetup.readyToJump)
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
            StartCoroutine(JumpReset());
        }
    }




    IEnumerator JumpReset()
    {
        yield return new WaitForSeconds(soPlayerSetup.jumpCoolDown);
        soPlayerSetup.readyToJump = true;
    }

    #endregion

    #region Falling

    void Falling()
    {
        if(!soPlayerSetup.grounded && soPlayerSetup.isFalling && !soPlayerSetup.gameOver)
        {
            playerAnim.GetAnimByType(PlayerAnimType.Fall);
        }
    }

    void Land()
    {
        VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.Jump, transform.position);
        playerAnim.GetAnimByType(PlayerAnimType.Land);

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

    #endregion

    #region Colliders

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Cutscene") && !soPlayerSetup.gameOver)
        {
            soPlayerSetup.readyToJump = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Cutscene") && !soPlayerSetup.gameOver)
        {
            soPlayerSetup.readyToJump = true;
        }
    }    

    private bool GroundCheck()
    {
        Debug.DrawLine(transform.position, Vector2.down, Color.red, distToGround + spaceToGround);
        return Physics2D.Raycast(transform.position, Vector2.down, distToGround + spaceToGround);
    }
    #endregion

    #region Animation

    void JumpAnimation()
    {
        playerAnim.GetAnimByType(PlayerAnimType.Jump);
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

    IEnumerator FallingAnimantion()
    {
        yield return new WaitForSeconds(soPlayerSetup.timeToFalling);
        soPlayerSetup.isFalling = true;
    }

    IEnumerator LandAnimation()
    {
        yield return new WaitForSeconds(soPlayerSetup.timeToLand);
        playerAnim.GetAnimByType(PlayerAnimType.Land, false);
    }


    void AwakeAnim()
    {
        soPlayerSetup.cutscene = true;
        playerAnim.GetAnimByType(PlayerAnimType.Awake);
    }


    public void DeadAnimation()
    {
        playerAnim.GetAnimByType(PlayerAnimType.Death);
    }

    #endregion
}
