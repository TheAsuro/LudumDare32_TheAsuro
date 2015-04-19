using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour, IResetObject
{
    private GameObject display;

    void Awake()
    {
        display = transform.parent.FindChild("Book").gameObject;
    }

    void Start()
    {
        GameInfo.gi.AddResetObject(this);
    }

    public void Pickup()
    {
        GameInfo.gi.TargetCollected();
        display.SetActive(false);
    }

    void IResetObject.Reset()
    {
        display.SetActive(true);
    }
}