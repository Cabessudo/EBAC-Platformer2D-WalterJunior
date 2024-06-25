using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAnimType
{
    Run,
    Attack,
    Death
}

public class EnemyAnim : AnimationBase<EnemyAnimType>
{}