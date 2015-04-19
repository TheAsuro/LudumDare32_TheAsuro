using UnityEngine;
using System.Collections;

public class EnemyTrigger : MonoBehaviour
{
    public EnemyScript parentScript;

    void Awake()
    {
        GameInfo.gi.AddTrigger(this);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            Vector3 direction = GameInfo.gi.player.transform.position - transform.position;
            if (!Physics.Raycast(transform.position, direction.normalized, direction.magnitude, GameInfo.gi.blockLayer))
            {
                parentScript.PlayerEntered();
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag.Equals("Player"))
            parentScript.PlayerLeft();
    }

    public void UpdatePlayerCollision()
    {
        if (parentScript.PlayerInside)
        {
            Vector3 direction = GameInfo.gi.player.transform.position - transform.position;
            if (Physics.Raycast(transform.position, direction.normalized, direction.magnitude, GameInfo.gi.blockLayer))
            {
                parentScript.PlayerLeft();
            }
        }
    }
}
