using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowPlayer : MonoBehaviour
{
    private GameObject _player;
    public Ease ease;
    public Ease easeEnd = Ease.Linear;
    private bool _start;
    private bool _theEnd;
    public float timeToStart = 1;
    public float duration = 1;
    public float playerDis = 3;
    public float yPos = -2.5f;
    public float yEndPos = 25;
    public float endDuration = 2; 
    public float delay = 2;
    public float delayEnd = 5;

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
        
        if(UIManager.Instance.index == 5 && !_theEnd)
        {
            StartCoroutine(EndAnim());   
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

    IEnumerator EndAnim()
    {
        transform.DOMoveY(yEndPos, endDuration).SetEase(easeEnd).SetDelay(delay);
        yield return new WaitForSeconds(delayEnd);
        UIManager.Instance.heartUI.SetActive(false);
        UIManager.Instance.coinsUI.SetActive(false);
        UIManager.Instance.endText.SetActive(true);
        _theEnd = true;
    }
}