using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechUI : UIManager
{
    public Player playerScript;
    public Fairy fairy;
    public LayerMask playerMask;
    public float radious = 3;
    public float waitAnim = 1;
    public bool onRadious;
    private bool _playOnceTYG = true;
    private bool _playOnceHW = true;


    // Update is called once per frame
    void Update()
    {
        showedText = speechText.text == sentences[index];
        Interact();
        ThereYouGoAnim();
        HandWaveAnim();

        if(onRadious && playerScript.soPlayerSetup.cutScene && fairy.awake && talkChance)
        StartCoroutine(WaitAnim());

        if(Input.GetKeyDown(KeyCode.Mouse0))
        NewText();
    }

    void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(fairy.transform.position, radious, playerMask);

        if(hit != null)
        {
            onRadious = true;
        }
        else
        {
            onRadious = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if(fairy != null)
        Gizmos.DrawWireSphere(fairy.transform.position, radious);
    }

    

    public void NewText()
    {
        if(showedText && isTalking)
        {
            if(index < sentences.Length - 1)
            {
               index++;
               speechText.text = "";
               StartCoroutine(TypeSentence());
            }
            else
            {
               speechText.text = "Ola";
               index = 0;
               dialogeText.SetActive(false);
               var heads = headProfile[headProfile.Length - 1];
               heads.SetActive(false);
               isTalking = false;
            }
        }
        else if(!showedText && isTalking)
        {
            speechText.text = "";
            speechText.text = sentences[index];
            StopAllCoroutines();
            showedText = true;
        }       
    }

    IEnumerator WaitAnim()
    {
        yield return new WaitForSeconds(waitAnim);
        Speech();
    }

    void ThereYouGoAnim()
    {
        if(index == 2 && _playOnceTYG)
        {
            if(fairy != null) fairy.anim.SetTrigger(fairy.triggerThereYouGo);
            _playOnceTYG = false;
        }  
    }

    void HandWaveAnim()
    {
        if(index == 5 && _playOnceHW)
        {
            if(fairy != null) fairy.anim.SetTrigger(fairy.triggerHandWave);
            _playOnceHW = false;
        }
    }
}
