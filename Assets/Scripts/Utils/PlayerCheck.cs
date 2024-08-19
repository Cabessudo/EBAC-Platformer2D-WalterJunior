using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    [Header("Check Player")]
    public LayerMask playerMask;
    public Transform playerPos;
    public float radius;
    public bool player;
    public bool checkOnce;

    void Start()
    {
        playerPos = Player.Instance.GetComponent<Transform>();
    }

    void Update()
    {
        CheckPlayer();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void CheckPlayer()
    {
        var check = Physics2D.OverlapCircle(transform.position, radius, playerMask);
        //Even if player exit the range dont stop, it's to the bosses
        if(check != null && checkOnce)
            player = true;

        //When player exit the range, the enemy stop atttack
        if(check != null && !checkOnce)
            player = true;
        else if(check == null && !checkOnce)
            player = false;
    }
}
