using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemCollatablesCoin : ItemCollatablesBase
{
    public GameObject PFB_collected;
    public SpriteRenderer coinSprite;
    public Ease ease = Ease.Linear;

    void Start()
    {
        Animation();
    }

    public override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddCoins();
        coinSprite.enabled = false;
        PFB_collected.SetActive(true);
        Destroy(gameObject, .3f);
    }

    void Animation()
    {
        transform.DOMoveY(transform.position.y + 0.5f, 1).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }
}
