using UnityEngine;
using System.Collections;

public class EnemyTrigger : MonoBehaviour
{
    public EnemyScript parentScript;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Player"))
            parentScript.PlayerEntered();
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag.Equals("Player"))
            parentScript.PlayerLeft();
    }
}
