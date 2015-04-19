using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    private bool knockedOut = false;
    private float knockedOutUntil;
    private Color originalColor;

    public bool KnockedOut { get { return knockedOut; } }

    void Awake()
    {
        Initialize();
    }

    protected void Initialize()
    {
        GameInfo.gi.AddEnemy(this, transform.FindChild("Collider").transform);
        originalColor = transform.FindChild("Collider").GetComponent<MeshRenderer>().materials[0].color;
    }

    void Update()
    {
        if (Time.time > knockedOutUntil && knockedOut)
        {
            EndKnockOut();
        }

        if (!knockedOut)
        {
            UpdateAggression();
        }
    }

    public void KnockOut(float duration)
    {
        knockedOut = true;
        knockedOutUntil = Time.time + duration;
        transform.FindChild("Collider").GetComponent<MeshRenderer>().materials[0].color = Color.black;
    }

    public void EndKnockOut()
    {
        knockedOut = false;
        transform.FindChild("Collider").GetComponent<MeshRenderer>().materials[0].color = originalColor;
    }

    public void PlayerEntered()
    {
        Aggress();
    }

    public void PlayerLeft()
    {
        EndAggress();
    }

    protected virtual void Aggress() {}

    protected virtual void EndAggress() { }

    protected virtual void UpdateAggression() { }
}
