using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollatablesBase : MonoBehaviour
{
    protected bool _chanceToCollect = true; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && _chanceToCollect)
        {
            _chanceToCollect = false;
            Collect();
        }
    }

    public virtual void Collect()
    {
        gameObject.SetActive(false);
        Debug.Log("Collected");
        OnCollect();
    }

    public virtual void OnCollect()
    {}
}
