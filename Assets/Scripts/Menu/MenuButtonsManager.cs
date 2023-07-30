using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuButtonsManager : MonoBehaviour
{

    public GameObject[] buttons;
    public float delay = 0.05f;
    public float duration = 0.2f;
    public Ease ease;

    // Start is called before the first frame update
    void OnEnable()
    {
        HideButtons();
        ShowButtons();
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
            b.transform.DOScale(1, duration).SetDelay(i * delay).SetEase(ease);   
        }
    }
}
