using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : HealthBase
{

    [Header("Hearts")]
    public Transform heartCase;
    [HideInInspector] public List<GameObject> hearts;
    public GameObject heartPref;
    public bool _checkHearts;


    public override void Start()
    {
        base.Start();
        CheckHeart();
    }
    
    public void UpdateHeartUI()
    {
        if(hearts.Count > 0)
        {
            for(int i = 0; i < hearts.Count; i++)
            {
                if(i < currLife)
                {
                    hearts[i].SetActive(true);
                }
                else
                {
                    hearts[i].SetActive(false);
                }
            }
        }
    }

    public override void Damage(int damage)
    {
        if(!soHealth.canHit) return;

        base.Damage(damage);
        UpdateHeartUI();        
        if(soHealth._isDead) return;
        flashColor?.Immune();
    }

    void CheckHeart()
    {
        for(int i = 0; i < soHealth.maxLife; i++)
        {
            var heart = Instantiate(heartPref, heartCase);
            hearts.Add(heart);
        }

        UpdateHeartUI();

    }

    public void AddHeart()
    {
        var heart = Instantiate(heartPref, heartCase);
        hearts.Add(heart);
        currLife++;
        UpdateHeartUI();
    }
}
