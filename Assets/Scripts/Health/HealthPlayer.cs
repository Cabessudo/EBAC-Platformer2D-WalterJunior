using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : HealthBase
{
    public GameObject[] hearts;
    public AudioSource hurtAudio;

    void Update()
    {
        HeartUI();
    }
    
    public void HeartUI()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < soHealth.currentLife)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);
        flashColor.Immune();
        hurtAudio.Play();
        HeartUI();
    }
}
