using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;//HERE
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    //Damage Color
    public List<SpriteRenderer> spriteRenderers;
    public Color color = Color.red;
    public Color immuneColor;
    public float duration = 0.1f;

    //Normal Color
    public SO_Health soHealth;
    private Color normalColor = Color.white;
    private Tween _currentTween;

    //Get The SpriteRenderer Of The Children In The Unity
    void OnValidate()
    {
        spriteRenderers = new List<SpriteRenderer>();
        foreach(var child in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderers.Add(child);
        }
    }

    //Change Character Color and His Children
    public void Flash()
    {
        if(_currentTween != null)
        {
            _currentTween.Kill();
            spriteRenderers.ForEach(i => i.color = normalColor);
        }

        foreach(var s in spriteRenderers)
        {
            _currentTween = s.DOColor(color, duration).SetLoops(2, LoopType.Yoyo);
        }
    }

    public void Immune()
    {
        StartCoroutine(ImmuneRoutine());
    }

    IEnumerator ImmuneRoutine()
    {
        soHealth.canHit = false;

        if(_currentTween != null)
            _currentTween.Kill();

        foreach(var s in spriteRenderers)
        {
            _currentTween = s.DOColor(immuneColor, duration).SetLoops(-1, LoopType.Yoyo);
        }

        yield return new WaitForSeconds(soHealth.timeImmune);

        soHealth.canHit = true;
        if(_currentTween != null)
        {
            _currentTween.Kill();
            spriteRenderers.ForEach(i => i.color = normalColor);
        }
    }

    public void Death()
    {
        if(soHealth._isDead)
        {
            _currentTween.Kill();
            spriteRenderers.ForEach(i => i.color = normalColor);
        }
    }
}
