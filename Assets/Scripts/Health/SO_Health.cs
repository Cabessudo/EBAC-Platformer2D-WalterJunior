using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_Health : ScriptableObject
{
    public int maxLife = 3;
    public bool destroyOnKill;
    public float delayToDie = 1;
    public float timeImmune = 1;
    public bool _isDead = false;
    public bool canHit = true;
}