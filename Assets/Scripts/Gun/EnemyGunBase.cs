using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyGunBase : MonoBehaviour
{
    public GameObject projectile;
    public Transform shootPos;

    public void Shoot(Action action = null)
    {
        action?.Invoke();
        Instantiate(projectile, shootPos.position, shootPos.rotation);
    }
}
