using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SOUIIntUpdate : MonoBehaviour
{
    public SOInt sOInt;
    public TextMeshProUGUI uiTextValue;

    // Start is called before the first frame update
    void Start()
    {
        uiTextValue.SetText(sOInt.value + " x");
    }

    public void UpdateValue()
    {
        uiTextValue.SetText(sOInt.value + " x");
    }
}
