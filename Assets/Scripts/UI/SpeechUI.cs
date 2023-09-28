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


    // Update is called once per frame
    void Update()
    {
        showedText = speechText.text == sentences[index];
        Interact();
        ThereYouGoAnim();

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
        if(speechText.text == sentences[2])
        {
            fairy.anim.SetTrigger(fairy.triggerThereYouGo);
        }
    }
}
