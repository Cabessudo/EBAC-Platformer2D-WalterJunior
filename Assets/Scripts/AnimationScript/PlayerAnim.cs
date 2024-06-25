using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimType
{
    Awake,
    Idle,
    Walk,
    Run,
    Jump,
    Fall,
    Land,
    Surprised,
    Death
}

public class PlayerAnim : AnimationBase<PlayerAnimType>
{}
