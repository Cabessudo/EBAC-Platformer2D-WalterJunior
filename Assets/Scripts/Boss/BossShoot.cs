using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    public GameObject projectile;
    public Transform shootPos;

    public void Shoot()
    {
        Instantiate(projectile, shootPos.position, shootPos.rotation);
    }
}
