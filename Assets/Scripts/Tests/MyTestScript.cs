using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyTestScript : MonoBehaviour
{

    public bool isAlive = true;
    public float duration;
    public Ease ease = Ease.Flash;

    // Start is called before the first frame update
    void Start()
    {
        DeadAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DeadAnimation()
    {
        transform.DORotate(new Vector3(0, 0, -90), duration, RotateMode.Fast).SetEase(ease);
    }
}
