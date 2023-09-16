using UnityEngine;

public class PlayerDestroyHelper : MonoBehaviour
{
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void DestroyPlayer()
    {
        Destroy(_player);
    }
}
