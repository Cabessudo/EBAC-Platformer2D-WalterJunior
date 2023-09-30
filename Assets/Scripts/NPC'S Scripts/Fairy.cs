using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fairy : MonoBehaviour
{

    [Header("References")]
    public Player player;
    public GameObject fairyBody;
    private GameObject _currentFairy;
    public Transform awakePos;
    public Animator anim;
    public string triggerThereYouGo = "ThereYouGo"; 
    public string triggerHandWave = "HandWave";
    public Ease awakeEase;
    public Ease xEase;
    public Ease yEase;
    public bool startMove;
    public bool isActive = false;
    public bool detectedPlayer = false;
    public bool awake = false;
    

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
    public float awakeDuration;
    

    //Delaies
    public float startDelay;
    public float stopDelay;
    public float finalDelay;
    public float awakeDelay;

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
        else 
        FinalStop();

        AwakeFairy();
    }

    #region Animation
    void YMove()
    {
        transform.DOMoveY(yMoveUp, yDuration).SetLoops(-1, LoopType.Yoyo).SetEase(yEase);
    }

    void StartAnim()
    {
        transform.DOMoveX(xStartAnim, startDuration).SetLoops(-1, LoopType.Yoyo).SetEase(xEase).SetDelay(startDelay);
        YMove();
    }

    void Stop()
    {
        transform.DOMoveX(xStop, normalDuration).SetEase(xEase).SetDelay(stopDelay);
    }

    void FinalStop()
    {
        transform.DOMoveX(xFinalStop, startFinalDuration).SetEase(xEase);
    }

    IEnumerator StartFinalMove()
    {
        yield return new WaitForSeconds(finalDelay);
        startMove = true;
    }
    #endregion

    void AwakeFairy()
    {
        awake = transform.position.x >= 65;
        if(awake)
        {
            transform.DOKill();
            if(!isActive && detectedPlayer)
            {
                isActive = true;
                player.soPlayerSetup.cutScene = true;
                StartCoroutine(PlayerAnim());
                _currentFairy = Instantiate(fairyBody);
                _currentFairy.transform.position = awakePos.position;
                _currentFairy.transform.DOScale(0, awakeDuration).SetEase(awakeEase).SetDelay(awakeDelay).From();
                anim = _currentFairy.GetComponent<Animator>();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            detectedPlayer = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            detectedPlayer = false;
        }
    }

    IEnumerator PlayerAnim()
    {
        yield return new WaitForSeconds(awakeDelay);
        player.currentPlayer.SetTrigger(player.soPlayerSetup.triggerSurprised);
    }
}
