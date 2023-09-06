using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{

    public Animator anim;
    public KeyCode keyToFly = KeyCode.A;
    public string  triggerToPlay = "Fly";

    private void OnValidate()
    {
        if(anim == null) anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Start()
    {
        anim.SetBool(triggerToPlay, true);
    }
}
