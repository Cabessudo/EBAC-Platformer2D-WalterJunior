using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossAnimType
{
    Shoot,
    Slam,
    Stunned,
    Idle,
    Death
}

public class BossAnim : AnimationBase<BossAnimType>
{}
