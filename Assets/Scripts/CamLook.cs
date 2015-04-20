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
                float xDelta = Input.GetAxis("Mouse X");

                transform.parent.Rotate(new Vector3(0f, xDelta * GameInfo.gi.sensitivity, 0f), Space.World);
            }
        }
    }

    private void FindPlayer()
    {
        if (GameInfo.gi.player != null)
            playerObject = GameInfo.gi.player;
    }
}
