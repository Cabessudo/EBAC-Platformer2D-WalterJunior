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
    private float timeToSetImmunity = .3f;

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
            _currentTween = s.DOColor(color, duration).SetLoops(2, LoopType.Yoyo).OnComplete(
                delegate
                {
                    spriteRenderers.ForEach(i => i.color = normalColor);
                });
        }
    }

    public void DisableAllSprites()
    {
        _currentTween?.Kill();

        foreach(var s in spriteRenderers)
        {
            s.enabled = false;                                                                                                                                                                                                                                                                        
        }
    }

    public void ChangeColor(Color c)
    {
        normalColor = c;
        _currentTween?.Kill();
        foreach(var s in spriteRenderers)
        {
            _currentTween = s.DOColor(normalColor, duration);
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

    #region  Player Immunity

    [NaughtyAttributes.Button]
    public void Immune()
    {
        StartCoroutine(ImmuneRoutine());
    }

    IEnumerator ImmuneRoutine()
    {
        soHealth.canHit = false;

        yield return new WaitForSeconds(timeToSetImmunity);

        //Remove the current sprite animation
        _currentTween?.Kill();

        //Add immune sprite animation
        foreach(var s in spriteRenderers)
        {
            _currentTween = s.DOColor(immuneColor, duration).SetLoops(-1, LoopType.Yoyo);
        }

        yield return new WaitForSeconds(soHealth.timeImmune);
        StopImmuneRoutine();
    }

    void StopImmuneRoutine()
    {
        _currentTween?.Kill();
        foreach(var s in spriteRenderers)
        {
            s.DOKill();
            _currentTween = s.DOColor(normalColor, duration); 
        }

        soHealth.canHit = true;
    }

    #endregion
}
