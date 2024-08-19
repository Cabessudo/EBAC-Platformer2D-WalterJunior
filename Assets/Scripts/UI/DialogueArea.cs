using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Dialogue;

public class DialogueArea : MonoBehaviour
{
    public DialogueType dialogueType;
    public TalkManager talkManager;
    public UnityEvent startEvent;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            startEvent?.Invoke();
            talkManager.Talk(dialogueType);
        }
    }
}
