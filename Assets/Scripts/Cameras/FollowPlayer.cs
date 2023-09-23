using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowPlayer : MonoBehaviour
{
    private GameObject _player;
    public Ease ease;
    private bool _start;
    public float timeToStart = 1;
    public float duration = 1;
    public float playerDis = 3;
    public float yPos = -2.5f;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        
        StartCoroutine(AnimStart());
    }
    // Update is called once per frame
    void Update()
    {
        if(_player != null && _start)
        StartFollowPlayer();
    }

    IEnumerator AnimStart()
    {
        transform.DOMoveY(yPos, duration).SetEase(ease);
        yield return new WaitForSeconds(timeToStart);
        _start = true;
    }

    void StartFollowPlayer()
    {
        transform.position = new Vector3(_player.transform.position.x, transform.position.y, transform.position.z);     
    }
}
