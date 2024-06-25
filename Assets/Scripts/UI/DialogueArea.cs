using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Dialogue;

public class DialogueArea : MonoBehaviour
{
    public DialogueType dialogueType;
    public TalkManager talkManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            talkManager.Talk(dialogueType);
        }
    }
}
