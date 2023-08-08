using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D _rb;
    private Vector2 velocity;
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float jumpForce;

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
            _rb.velocity = new Vector2(speed, _rb.velocity.y);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            _rb.velocity = new Vector2(-speed, _rb.velocity.y);
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
        }
    }
}
