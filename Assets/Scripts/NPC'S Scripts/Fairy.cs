using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Dialogue;

public class Fairy : MonoBehaviour
{

    [Header("References")]
    public Player player;
    public GameObject fairyBody;

    public Ease awakeEase;
    public Ease xEase;
    public bool awake = false;
    

    [Header("Move Parameters")]
    public float xStartAnim;
    public float xFirstStop;
    public float xFinalStop;

    //Durations
    public float normalDuration = 1;
    public float startFinalDuration = 8;
    public float awakeDuration;

    //Delays
    public float firstStopDelay;
    public float awakeDelay;

    // Start is called before the first frame update
    void Start()
    {
        AwakePlayerAnim();
    }

    #region Animation

    void AwakePlayerAnim()
    {
        transform.DOMoveX(xStartAnim, normalDuration).SetLoops(-1, LoopType.Yoyo).SetEase(xEase);
        Invoke(nameof(FirstStop), firstStopDelay);
    }

    void FirstStop()
    {
        transform.DOMoveX(xFirstStop, normalDuration).SetEase(xEase).OnComplete(
            delegate{ AwakeFairy(); });
    }

    public void FinalStop()
    {
        HideFairy();
        transform.DOMoveX(xFinalStop, startFinalDuration).SetEase(xEase).SetDelay(awakeDuration);
        player.soPlayerSetup.cutScene = false;
    }

    public void ShowFairy()
    {
        transform.DOKill();
        fairyBody.transform.DOScale(Vector3.one, awakeDuration).SetEase(awakeEase);
    }

    void HideFairy()
    {
        fairyBody.transform.DOScale(Vector3.zero, awakeDuration).SetEase(awakeEase);
    }
    #endregion

    void AwakeFairy()
    {
        if(!awake)
        {
            awake = true;
            ShowFairy();
            StartCoroutine(AwakeRoutine());
        }
    }

    IEnumerator AwakeRoutine()
    {
        yield return new WaitForSeconds(awakeDuration);
        player.playerAnim.GetAnimByType(PlayerAnimType.Surprised);
        TalkManager.Instance.Talk(DialogueType.First_Dialogue);
    }
}
