using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayHelper : MonoBehaviour
{
    public AudioSource audioSource;
    public KeyCode keyCode = KeyCode.Q; 

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyCode))
        {
            audioSource.Play();
        }
    }
}
