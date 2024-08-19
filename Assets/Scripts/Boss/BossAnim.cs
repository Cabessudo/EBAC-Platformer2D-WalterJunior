using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossAnimType
{
    Shoot,
    Slam,
    Stunned,
    Idle,
    Death,
    Angry
}

public class BossAnim : AnimationBase<BossAnimType>
{}
