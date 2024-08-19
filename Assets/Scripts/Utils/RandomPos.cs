using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPos : MonoBehaviour
{
    public Transform parentTransform;
    public float maxRandomX;
    public float randomX;
    public float speed;

    [NaughtyAttributes.Button]
    public void RandomPosition()
    {
        StopAllCoroutines();
        StartCoroutine(RandomPositionRoutine());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    IEnumerator RandomPositionRoutine()
    {
        randomX = Random.Range(0.11f, maxRandomX);
        Vector3 randomPos = new Vector3(parentTransform.position.x - randomX, transform.position.y, parentTransform.position.z);
        while(Vector2.Distance(transform.position, randomPos) > .1f)
        {
            transform.position = Vector3.Lerp(transform.position, randomPos, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
    }
}
