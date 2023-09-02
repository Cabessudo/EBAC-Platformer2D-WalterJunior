using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Video;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{

    [Header("References")]
    private Rigidbody2D _rb;

    [Header("Movement")]
    private Vector2 velocity;
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float jumpForce;

    [Header("Animation")]
    //Jump
    public float jumpDuration;
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.5f;
    public Ease easeIn = Ease.InBack;
    public Ease easeOut = Ease.OutBack;
    //Fall
    public bool grounded;
    public float fallDuration;
    public float fallX;
    public float fallY;

    [Header("Live & Death")]
    public Image[] hearts;
    

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Movement();
        Falling();
        HeartUI();
    }

    void Movement()
    {
        if(Input.GetKey(KeyCode.D))
        {
            _rb.velocity = new Vector2(Input.GetKey(KeyCode.LeftShift) ? speedRun : speed, _rb.velocity.y);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            _rb.velocity = new Vector2(Input.GetKey(KeyCode.LeftShift) ? -speedRun : -speed, _rb.velocity.y);
        }

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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity = Vector2.up * jumpForce;
            _rb.transform.localScale = Vector2.one;
            grounded = false;

            _rb.transform.DOKill();
            
            JumpAnimation();
        }
    }

    void JumpAnimation()
    {
        _rb.transform.DOScaleY(jumpScaleY, jumpDuration/2).SetEase(easeIn).OnComplete(
            delegate
            {
                _rb.transform.DOScaleY(1, jumpDuration/2).SetEase(easeOut);
            });

        _rb.transform.DOScaleX(jumpScaleX, jumpDuration/2).SetEase(easeIn).OnComplete(
            delegate
            {
                _rb.transform.DOScaleX(1, jumpDuration/2).SetEase(easeOut);
            });
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") && !grounded)
        {
            Fall();
            grounded = true;
        }
    }

    void Fall()
    {
        _rb.transform.DOKill();

        _rb.transform.DOScaleX(fallX, fallDuration/2).SetEase(easeIn).OnComplete(
            delegate
            {
                _rb.transform.DOScaleX(1, fallDuration/2).SetEase(easeOut);
            });

        _rb.transform.DOScaleY(fallY, fallDuration/2).SetEase(easeIn).OnComplete(
            delegate
            {
                _rb.transform.DOScaleY(1, fallDuration/2).SetEase(easeOut);
            });
    }

    void Falling()
    {
        if(!grounded)
        {
            StartCoroutine(FallingAnimation());
        }
        else
        {
            _rb.transform.localScale = new Vector2(1, 1);
            StopAllCoroutines();
        }
    }

    IEnumerator FallingAnimation()
    {
        yield return new WaitForSeconds(5);
        _rb.transform.localScale = new Vector2(jumpScaleX, jumpScaleY);
    }

    void HeartUI()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < HealthBase.Instance.currentLife)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
