using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossShadowAnim : MonoBehaviour
{
    private Vector2 startSize;
    public Transform bossPos;
    public float duration;
    private 

    void Start()
    {
        startSize = transform.localScale;
    }

    void Update()
    {
        if(bossPos != null)
            transform.position = new Vector3(bossPos.position.x, transform.position.y, transform.position.z);
        else
            Destroy(gameObject);
    }

    public void Fade()
    {
        transform.DOScale(Vector2.zero, duration);
    } 

    public void Appear()
    {
        transform.DOScale(startSize, duration);
    }

    void OnDestroy()
    {
        transform.DOKill();
    }
}