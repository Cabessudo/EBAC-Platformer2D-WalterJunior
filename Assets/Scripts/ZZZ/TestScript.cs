using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    [NaughtyAttributes.Button]
    public void Button()
    {
        StopAllCoroutines();
        StartCoroutine(Routine());
    }

    IEnumerator Routine()
    {
        yield return null;
    }
}

