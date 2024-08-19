using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAnimScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Ease ease = Ease.Linear;
    public float scale = 1.1f;
    public float duration = 1;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(scale, duration).SetEase(ease);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
}
