using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIPause : MonoBehaviour
{
    public GameObject menuScreen;
    public TextMeshProUGUI pauseAlertText;
    private KeyCode _keyPause = KeyCode.Escape;
    private Ease _ease = Ease.Linear;
    public bool pause = false;
    public float durationToHide = 3;

    void Start()
    {
        ColorBlink();
        StartCoroutine(HidePauseText());
    }

    void Update()
    {
        if(Input.GetKeyDown(_keyPause))
        Pause();
    }

    public void Pause(bool b = true)
    {
        pause = b;

        if(pause)
        menuScreen.SetActive(true);
        else
        menuScreen.SetActive(false);
    }

    public void ColorBlink()
    {
        pauseAlertText.DOColor(new Color(1,1,1,.1f), 1.5f).SetLoops(-1, LoopType.Yoyo).SetEase(_ease);
    }

    IEnumerator HidePauseText()
    {
        yield return new WaitForSeconds(durationToHide);
        pauseAlertText.DOKill();
        pauseAlertText.enabled = false;
    }
}
