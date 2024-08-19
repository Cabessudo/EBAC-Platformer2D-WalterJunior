using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableHeart : ItemCollatablesBase
{
    public float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        IdleAnim();
    }

    void IdleAnim()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    public override void Collect()
    {
        foreach(var sprites in graphicItem.GetComponentsInChildren<SpriteRenderer>())
        {
            sprites.enabled = false;    
        }

        Player.Instance.playerHealth.AddHeart();
        base.Collect();
    }
}
