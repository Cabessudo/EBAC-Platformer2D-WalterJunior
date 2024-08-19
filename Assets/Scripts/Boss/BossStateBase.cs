using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateBase : StateBase
{
    public BossBase boss;

    public override void OnEnterState(object obj)
    {
        base.OnEnterState(obj);
        boss = (BossBase)obj;
    }
}

public class BossFirstAttackState : BossStateBase
{

}


