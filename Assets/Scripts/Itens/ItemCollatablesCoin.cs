using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollatablesCoin : ItemCollatablesBase
{
    public override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddCoins();
    }
}
