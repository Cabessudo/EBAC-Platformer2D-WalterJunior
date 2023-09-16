using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SOUIIntUpdate : MonoBehaviour
{
    public SOInt sOInt;
    public TextMeshProUGUI uiTextValue;

    public void UpdateValue()
    {
        uiTextValue.SetText(sOInt.value + " x");
    }
}
