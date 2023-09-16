using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{

    [SerializeField] Animator _anim;
    public ProjectileBase PFB_projectile;
    private GameObject _playerDirection;
    public Transform shootPos;
    private Coroutine _currentCoroutine;
    public KeyCode keyToShoot = KeyCode.F;
    public float timeBetweenShoot = .2f;
    public string triggerToShoot = "Shooting";
    public bool _isShooting;

    void Start()
    {
        _playerDirection = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(Input.GetKeyDown(keyToShoot))
        {
            _isShooting = true;
            _currentCoroutine = StartCoroutine(ShootRotine());
        }        
        else if(Input.GetKeyUp(keyToShoot))
        {
            _isShooting = false;
            _anim.SetBool(triggerToShoot, false);
            StopCoroutine(_currentCoroutine);
        }
    }

    void Shoot()
    {
        var projectile = Instantiate(PFB_projectile);
        projectile.transform.position = shootPos.position;
        projectile.side = _playerDirection.transform.localScale.x;
    }

    IEnumerator ShootRotine()
    {
        while(_isShooting)
        {
            Shoot();
            _anim.SetBool(triggerToShoot, true);
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }
}
