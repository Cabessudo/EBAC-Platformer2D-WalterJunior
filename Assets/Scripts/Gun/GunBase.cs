using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{

    [SerializeField] Animator _anim;
    public ProjectileBase PFB_projectile;
    public Player player;
    public AudioShootRandomPlay audioShoot;
    public Transform shootPos;
    private Coroutine _currentCoroutine;
    public KeyCode keyToShoot = KeyCode.F;
    public float timeBetweenShoot = .2f;
    public string triggerToShoot = "Shooting";
    public bool _isShooting;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update()
    {

        if(!UIPause.Instance.pause && !player.soPlayerSetup.cutscene && !player.playerHealth.soHealth._isDead)
        {
            if(Input.GetKeyDown(keyToShoot))
            {
                Shoot();
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
        
        if(player.playerHealth.soHealth._isDead || player.soPlayerSetup.cutscene)
        {
            StopShoot();
        }
    }

    public void StopShoot()
    {
        if(_currentCoroutine != null)
        StopCoroutine(_currentCoroutine);
    }

    void Shoot()
    {
        var projectile = Instantiate(PFB_projectile);
        projectile.transform.position = shootPos.position;
        projectile.speed = player.transform.localScale.x;
        if(audioShoot != null) audioShoot.PlayAudioRandomShoot();
    }

    IEnumerator ShootRotine()
    {
        yield return new WaitForSeconds(timeBetweenShoot);
        while(_isShooting)
        {
            Shoot();
            _anim.SetBool(triggerToShoot, true);
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }
}
