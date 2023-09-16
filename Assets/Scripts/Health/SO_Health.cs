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
    public float delayToDie = 1;
}
