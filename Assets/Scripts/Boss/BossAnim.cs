using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossAnimType
{
    Shoot,
    Slam,
    Stunned,
    Idle
}

public class BossAnim : AnimationBase<BossAnimType>
{}
