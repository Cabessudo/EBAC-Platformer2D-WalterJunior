using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Camera;

public class ChangeCamArea : MonoBehaviour
{
    public CamerasGame camManager;
    public CamType camType;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            camManager.ChangeCamByType(camType);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            camManager.ChangeCamByType(CamType.Player);
    }
}
