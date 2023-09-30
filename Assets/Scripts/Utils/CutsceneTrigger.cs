using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CutsceneTrigger : MonoBehaviour
{
    public AudioMixerSnapshot snapshotGame;
    public AudioMixerSnapshot snapshotMenu;
    public float transitionTime = .15f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            snapshotMenu.TransitionTo(transitionTime);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            snapshotGame.TransitionTo(transitionTime);
        }
    }
}
