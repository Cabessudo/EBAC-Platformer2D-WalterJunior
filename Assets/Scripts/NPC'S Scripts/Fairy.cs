using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fairy : MonoBehaviour
{
    
    private Tween _yTween;
    private Tween _xTween;
    public Ease xEase;
    public Ease yEase;
    public bool detectedPlayer;
    public bool startMove;
    

    [Header("Move Parameters")]
    public float yMoveUp;
    public float yMoveDown;
    public float xStartAnim;
    public float xStop;
    public float xFinalStop;

    //Durations
    public float yDuration = .1f;
    public float startDuration = 1;
    public float normalDuration = 1;
    public float startFinalDuration = 8;
    

    //Dealaies
    public float startDelay;
    public float stopDelay;
    public float finalDelay;

    // Start is called before the first frame update
    void Start()
    {
        StartAnim();
        StartCoroutine(StartFinalMove());
    }

    // Update is called once per frame
    void Update()
    {
        if(!startMove)
        Stop();
    }

    void StartAnim()
    {
        transform.DOMoveX(xStartAnim, startDuration).SetLoops(-1, LoopType.Yoyo).SetEase(xEase).SetDelay(startDelay);
        _yTween = transform.DOMoveY(yMoveUp, yDuration).SetLoops(-1, LoopType.Yoyo).SetEase(yEase);
    }

    void Stop()
    {
        transform.DOMoveX(xStop, normalDuration).SetEase(xEase).SetDelay(stopDelay);
    }

    void FinalStop()
    {
        _xTween = transform.DOMoveX(xFinalStop, startFinalDuration).SetEase(xEase);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && startMove && detectedPlayer)
        {
            FinalStop();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && startMove)
        {
            detectedPlayer = true;
        }
    }
    

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && startMove)
        {
            detectedPlayer = false;
            _xTween.Kill();
        }
    }

    IEnumerator StartFinalMove()
    {
        yield return new WaitForSeconds(finalDelay);
        startMove = true;
    }
}
