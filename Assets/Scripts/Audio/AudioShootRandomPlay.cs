using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioShootRandomPlay : MonoBehaviour
{
    public AudioSource shootAudio;
    public AudioClip[] randomAudios;
    private int _index;

    public void PlayAudioRandomShoot()
    {
        if(randomAudios.Length >= _index) _index = 0;

        shootAudio.clip = randomAudios[_index];
        shootAudio.Play();
        _index++;    
    }
}
