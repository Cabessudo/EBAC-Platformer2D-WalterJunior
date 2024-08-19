using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Color defaultColor;
    public Color secondColor;
    public bool colorChanged;
    
    void Start ()
    {
        colorChanged = false;
    }

    void OnEnable()
    {    
        if(!colorChanged)
            sprite.color = defaultColor;
        
        if(colorChanged)
            Change();
    }

    public void Change()
    {
        colorChanged = true;
        sprite.color = secondColor;
    }
}
