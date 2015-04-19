using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public bool canMove;

    private PlayerTeleport teleportScript;
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Awake()
    {
        GameInfo.gi.player = gameObject;
        teleportScript = GetComponent<PlayerTeleport>();
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        if (canMove && Camera.main != null && !teleportScript.ChargingTeleport)
        {
            Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            Quaternion camRotation = Camera.main.transform.rotation;
            Vector3 movementDirection = camRotation * inputDirection;

            GetComponent<Rigidbody>().velocity = new Vector3(movementDirection.x, 0f, movementDirection.z) * moveSpeed;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Target"))
        {
            col.GetComponent<Target>().Pickup();
        }

        if (col.tag.Equals("Goal"))
        {
            GameInfo.gi.GoalEntered();
        }
    }

    public void ResetPlayer()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
