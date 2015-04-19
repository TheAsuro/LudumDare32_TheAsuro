using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    public void Pickup()
    {
        GameInfo.gi.TargetCollected();
        GameObject.Destroy(transform.parent.gameObject);
    }
}
