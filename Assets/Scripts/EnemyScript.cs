using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    private bool knockedOut = false;
    private float knockedOutUntil;
    private Color originalColor;

    protected bool aggressing = false;
    protected int aggressionCounter = 0;

    public bool KnockedOut { get { return knockedOut; } }
    public bool PlayerInside { get { return aggressing; } }

    protected Vector3 startPosition;
    protected Quaternion startRotation;

    void Awake()
    {
        originalColor = transform.FindChild("Collider").GetComponent<MeshRenderer>().materials[0].color;
        startPosition = transform.position;
        startRotation = transform.rotation;
        Initialize();
    }

    void Start()
    {
        GameInfo.gi.AddEnemy(this, transform.FindChild("Collider").transform);
        LateInitialize();
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
        aggressionCounter++;
        Aggress();
    }

    public void PlayerLeft()
    {
        aggressionCounter--;
        if (aggressionCounter < 0)
            aggressionCounter = 0;
        EndAggress();
    }

    public void Reset()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        aggressionCounter = 0;
        aggressing = false;
        EndKnockOut();
        ResetEnemy();
    }

    protected virtual void Aggress() {}

    protected virtual void EndAggress() { }

    protected virtual void UpdateAggression() { }

    protected virtual void ResetEnemy() { }

    protected virtual void Initialize() { }

    protected virtual void LateInitialize() { }
}
