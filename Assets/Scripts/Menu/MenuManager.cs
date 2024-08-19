using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [Header("References")]
    public GameObject settingsCase;
    public List<Transform> buttons; 
    public List<Transform> settings;

    [Header("Buttons Anim")]
    public Ease ease = Ease.Linear;
    public float duration = 0.2f;

    [Header("Change Scene")]
    public LoadScene loadScene;
    public Image changeScene;
    public int scene = 1;
    private int changeDuration = 1;

    public void ShowMainButtons()
    {
        settings.ForEach(i => i.DOScale(0, duration).SetEase(ease));
        buttons.ForEach(i => i.DOScale(1, duration).SetEase(ease));
    }

    public void ShowSettings()
    {
        settingsCase.SetActive(true);
        buttons.ForEach(i => i.DOScale(0, duration).SetEase(ease));
        settings.ForEach(i => i.DOScale(1, duration).SetEase(ease));
    }

    public void ChangeScene()
    {
        changeScene.DOColor(Color.black, changeDuration).SetEase(ease).OnComplete(
            delegate{ loadScene.Load(scene);});
    }
}
