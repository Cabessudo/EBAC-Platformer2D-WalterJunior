using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Player.Instance != null)
        transform.position = Player.Instance.transform.position;       
    }
}
