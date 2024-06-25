using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_Health : ScriptableObject
{
    public int _life = 3;
    public int currentLife;
    public bool destroyOnKill;
    public bool _isDead = false;
    public bool canHit = true;
    public float delayToDie = 1;
    public float timeImmune = 1;
}
