using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBase<T> : MonoBehaviour where T : System.Enum 
{
    public List<AnimSetup> animSetups;
    public Animator anim;

    public void GetAnimByType(T currType)
    {
        var setup = animSetups.Find(i => i.type.ToString() == currType.ToString());
        anim.SetTrigger(setup.animTrigger);
    }

    [System.Serializable]
    public class AnimSetup
    {
        public T type;
        public string animTrigger;
    }
}
