using UnityEngine;

public class PlayerDestroyHelper : MonoBehaviour
{
    public Player player;

    public void DestroyPlayer()
    {
        Destroy(player.gameObject);
    }
}
