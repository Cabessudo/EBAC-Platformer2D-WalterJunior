using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    public GameObject dialogeText;
    public GameObject[] headProfile;
    public TextMeshProUGUI speechText;

    [Header("Settings")]
    public float textSpeed;
    public string[] sentences;
    public int index;
    public bool isTalking = false;
    public bool showedText;
    public bool talkChance = true;

    public void Speech()
    {
        talkChance = false;
        isTalking = true;
        dialogeText.SetActive(true);
        var heads = headProfile[headProfile.Length - 1];
        heads.SetActive(true);
    } 

    public IEnumerator TypeSentence()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
