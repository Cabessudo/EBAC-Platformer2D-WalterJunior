using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowPlayer : MonoBehaviour
{
    private GameObject _player;
    public GameUI gameUI;
    public Ease ease;
    public Ease easeEnd = Ease.Linear;
    public bool cutscene;
    private bool _start;
    public float timeToStart = 1;
    public float duration = 1;
    public float yPos = -2.5f;
    public float yEndPos = 25;
    public float endDuration = 2; 
    public float delay = 2;
    public float delayEnd = 5;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if(cutscene)
        {
            Player.Instance.soPlayerSetup.cutscene = true;
            StartCoroutine(AnimStart());
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(_start || !cutscene)
        {
            if(_player != null )
                StartFollowPlayer();   
        }
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

    public void TheEnd()
    {
        StartCoroutine(EndAnimRoutine());
    }

    IEnumerator EndAnimRoutine()
    {
        transform.DOMoveY(yEndPos, endDuration).SetEase(easeEnd).SetDelay(delay);
        yield return new WaitForSeconds(delayEnd);
        gameUI.ShowEndUI();
    }
}