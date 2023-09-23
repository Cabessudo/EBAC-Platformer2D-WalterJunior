using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class MenuManager : MonoBehaviour
{
    public List<animMenu> animMenus;

    public void OnMenu(string animParameter)
    {
        foreach(var a in animMenus)
        {
            if(a.animParameter == animParameter)
            {
                a.off = false;
                a.anim.SetBool(a.animParameter, a.off);
            }
        }
    }

    public void OffMenu(string animParameter)
    {
        foreach(var a in animMenus)
        {
            if(a.animParameter == animParameter)
            {
                a.off = true;
                a.anim.SetBool(a.animParameter, a.off);
            }
        }
    }

    [System.Serializable]
    public class animMenu
    {
        public Animator anim;
        public string animParameter;
        public bool off;
    }
}
