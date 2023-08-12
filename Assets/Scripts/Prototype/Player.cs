using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

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
    public float duration;
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.5f;
    public Ease ease = Ease.OutBack;

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
            _rb.velocity = Vector3.up * jumpForce;
            _rb.transform.localScale = Vector2.one;

            DOTween.Kill(_rb.transform);
            
            JumpAnimation();
        }
    }

    void JumpAnimation()
    {
        _rb.transform.DOScaleY(jumpScaleY, duration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        _rb.transform.DOScaleX(jumpScaleX, duration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }
}
