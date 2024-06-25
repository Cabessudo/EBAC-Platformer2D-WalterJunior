using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ebac.Core.Singleton;

namespace Dialogue
{
    public enum DialogueType
    {
        First_Dialogue,
        Second_Dialogue
    }

    public class DialogueManager : Singleton<DialogueManager>
    {
        public List<DialogeSetup> dialogeSetups;

        public DialogeSetup GetDialogueByType(DialogueType dialogueType)
        {
            return dialogeSetups.Find(i => i.type == dialogueType);
        }

    }

    [System.Serializable]
    public class DialogeSetup
    {
        public DialogueType type;
        public List<string> sentences;
        public UnityEvent firstEvent;
        public UnityEvent lastEvent;
    }
}
