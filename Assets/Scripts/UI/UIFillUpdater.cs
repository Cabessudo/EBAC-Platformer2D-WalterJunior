using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIFillUpdater : MonoBehaviour
{
    public Image image;

    [Header("Anim")]
    private Tween _currTween;
    private Ease ease = Ease.Linear;
    public float duration = .1f;

    void OnValidate()
    {
        if(image == null) image = GetComponent<Image>();        
    }

    public virtual void UpdateValue(float f)
    {
        image.fillAmount = f;
    }

    //Fill
    public virtual void UpdateValue(float curr, float max)
    {
        if(_currTween != null) _currTween.Kill();
        image.DOFillAmount(1 - (curr/max), duration).SetEase(ease);
    }

    //Unfill
    public void UpdateValueEmpty(float curr, float max)
    {
        if(_currTween != null) _currTween.Kill();
        image.DOFillAmount(curr/max, duration).SetEase(ease);
    }

    public void UpdateValueFill(float duration)
    {
        if(_currTween != null) _currTween.Kill();
        image.DOFillAmount(1, duration).SetEase(ease).OnComplete(
            delegate{image.transform.DOScale(1, .1f).SetEase(ease);});
    } 
}
