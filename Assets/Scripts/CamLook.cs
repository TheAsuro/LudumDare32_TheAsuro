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
            transform.LookAt(playerObject.transform);
    }

    private void FindPlayer()
    {
        if (GameInfo.gi.player != null)
            playerObject = GameInfo.gi.player;
    }
}
