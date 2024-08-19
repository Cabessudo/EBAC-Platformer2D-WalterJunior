using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : EnemyBase
{
    public Rigidbody2D _rb;

    public override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();
    }

    public override void Attack()
    {
        StopAllCoroutines();
        if(check.playerPos != null)
        {
            var chasePos = new Vector3(check.playerPos.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, chasePos, Time.deltaTime * speed);
        }
    }

    [NaughtyAttributes.Button]
    public override void OnEnemyDeath()
    {
        _rb.velocity = Vector3.zero;
        base.OnEnemyDeath();
    }
}
