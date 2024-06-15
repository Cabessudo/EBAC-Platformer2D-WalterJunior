using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossAnimType
{
    Shoot,
    Jump,
    Slam,
    Stunned
}

public class BossAnim : AnimationBase<BossAnimType>
{
}
