using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollatablesBase : MonoBehaviour
{
    public GameObject PFB_collected;
    public SpriteRenderer graphicItem;
    public ParticleSystem PFB_particleSystem;
    protected bool _chanceToCollect = true; 
    public float durationToHide = 1;
    
    [Header("Sounds")]
    public AudioSource audioSource;

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
        Debug.Log("Collected");
        if(graphicItem != null) graphicItem.enabled = false;
        
        OnCollect();
    }

    public virtual void OnCollect()
    {
        if(PFB_collected != null) PFB_collected.SetActive(true);
        if(PFB_particleSystem != null) Instantiate(PFB_particleSystem, transform.position, PFB_particleSystem.transform.rotation);
        if(audioSource != null) audioSource.Play();
        Destroy(gameObject, .3f);
    }
}
