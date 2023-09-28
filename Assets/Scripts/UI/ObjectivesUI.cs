using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ObjectivesUI : MonoBehaviour
{
    public Fairy fairy;
    public Animator anim;
    public TextMeshProUGUI objectiveText;
    public Ease ease = Ease.Linear;
    public float textScale = 1.3f;
    public float duration = 1;
    public float delay = 7;
    public float timeToDisable = 3;
    private string triggerToComplete = "Complete";

    // Start is called before the first frame update
    void Start()
    {
        objectiveText.DOColor(new Color(255, 227, 0, 1), duration * 2).SetDelay(delay).SetEase(ease);
        objectiveText.transform.DOScale(textScale, duration).SetLoops(-1, LoopType.Yoyo).SetEase(ease).SetDelay(delay);
    }

    // Update is called once per frame
    void Update()
    {
        if(fairy.isActive)
        {
            anim.SetTrigger(triggerToComplete);
            StartCoroutine(Disable());
        }
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(timeToDisable);
        objectiveText.gameObject.SetActive(false);
    }
}
