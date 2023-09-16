using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : HealthBase
{
    public Image[] hearts;

    public void HeartUI()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < soHealth.currentLife)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);
        HeartUI();
    }
}
