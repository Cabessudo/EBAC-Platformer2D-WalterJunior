using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using TMPro;
using Ebac.Core.Singleton;

public class TalkManager : Singleton<TalkManager>
{
    [Header("References")]
    protected DialogeSetup _currDialogue;
    public GameObject dialogeImage; //Talk Hud
    public TextMeshProUGUI speechText;


    [Header("Settings")]
    public float textSpeed;
    public int indexSentence;
    private bool isTalking = false;
    private bool showedText; 
    private bool talkChance = true;

    void Update()
    {
        if(_currDialogue != null)
        showedText = speechText.text == _currDialogue.sentences[indexSentence];

        if(Input.GetKeyDown(KeyCode.Mouse0))
        NextText();
    }


    public void Talk(DialogueType dialogueType)
    {
        _currDialogue = DialogueManager.Instance.GetDialogueByType(dialogueType);
        StartDialogue();
        StartCoroutine(TypeSentence());
    }

    //Type Animation
    public IEnumerator TypeSentence()
    {
        foreach(char letter in _currDialogue.sentences[indexSentence].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    //Pass To the next Text
    public void NextText()
    {
        if(showedText && isTalking ) //Still talking and the whole text is showed
        {
            if(indexSentence < _currDialogue.sentences.Count - 1) //When there are more dialogues left
            {
               speechText.text = "";
               indexSentence++;
               StartCoroutine(TypeSentence());
            }
            else //When aren't
            {
               EndDialogue();
            }
        }
        else if(!showedText && isTalking) //When the text is not showed yet but the player want to jump the type animation 
        {
            ShowAllDialogue();
        }       
    }

    void StartDialogue()
    {
        _currDialogue.firstEvent?.Invoke();
        speechText.text = "";
        talkChance = false; //To not double trigger the dialogue
        isTalking = true;
        dialogeImage.SetActive(true);
    } 

    void ShowAllDialogue()
    {
        //Clean to show all text
        StopAllCoroutines();
        speechText.text = "";
        speechText.text = _currDialogue.sentences[indexSentence]; 
        showedText = true;
    }
    
    void EndDialogue()
    {
        _currDialogue.lastEvent?.Invoke();
        talkChance = true;
        isTalking = false;
        dialogeImage.SetActive(false);

        //Clean the sentences settings to the next
        speechText.text = "";
        indexSentence = 0;
    }
}
