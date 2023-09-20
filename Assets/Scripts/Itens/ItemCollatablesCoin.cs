using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemCollatablesCoin : ItemCollatablesBase
{
    public Ease ease = Ease.Linear;

    void Start()
    {
        Animation();
    }

    public override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddCoins();
        // coinSprite.enabled = false;
        // Instantiate(PS_Coin, transform.position, PS_Coin.transform.rotation);
        // Destroy(gameObject, .3f);
    }

    void Animation()
    {
        transform.DOMoveY(transform.position.y + 0.5f, 1).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }
}
