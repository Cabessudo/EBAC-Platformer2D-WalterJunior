using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuButtonsManager : MonoBehaviour
{

    public GameObject[] buttons;
    public float delay = 0.05f;
    public float duration = 0.2f;
    public float animDuration = 1;
    public float animDelay = 1;
    public float scale = 2;
    public Ease ease;

    // Start is called before the first frame update
    void OnEnable()
    {
        HideButtons();

        ShowButtons();
    }

    void Start()
    {
        ButtonAnim();
    }

    void HideButtons()
    {
        foreach(var g in buttons)
        {
            g.transform.localScale = Vector3.zero;
            g.SetActive(false);
        }
    }

    void ShowButtons()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            var b = buttons[i];
            b.SetActive(true);
            b.transform.DOScale(scale, duration).SetDelay(i * delay).SetEase(ease);   
        }
    }

    void ButtonAnim()
    {
        foreach(var b in buttons)
        {
            b.transform.DOScale(scale - .3f, animDuration).SetDelay(animDelay).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
        }
    }
}
