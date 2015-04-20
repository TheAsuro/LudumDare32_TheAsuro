using UnityEngine;
using System.Collections;

public class CamLook : MonoBehaviour
{
    private GameObject playerObject;

    void Awake()
    {
        FindPlayer();
    }

    void Update()
    {
        if (playerObject == null)
            FindPlayer();
        else
        {
            if (!GameInfo.gi.Paused)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(playerObject.transform.position.x, transform.position.y, playerObject.transform.position.z), 0.01f);
            }
        }
    }

    private void FindPlayer()
    {
        if (GameInfo.gi.player != null)
            playerObject = GameInfo.gi.player;
    }
}
