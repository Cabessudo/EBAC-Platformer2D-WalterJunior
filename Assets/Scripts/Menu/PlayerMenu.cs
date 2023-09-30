using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    public Animator anim;
    public string triggerJumpIdle = "JumpIdle";
    public string triggerWalkIdle = "WalkIdle";
    public bool walk;

    void Start()
    {
        if(walk)
        anim.SetTrigger(triggerWalkIdle);
        else
        anim.SetTrigger(triggerJumpIdle);
    }
}
