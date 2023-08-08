using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D _rb;
    private Vector2 velocity;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
