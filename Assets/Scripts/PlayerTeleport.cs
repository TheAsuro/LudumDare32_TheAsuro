using UnityEngine;
using System.Collections;

public class PlayerTeleport : MonoBehaviour
{
    public bool canTeleport;
    public LayerMask teleportLayers;
    public float teleportDelay;
    public float knockoutRange;
    public float knockoutDuration;
    public float jumpHitTimeoutDuration;
    public float leftClickEarlyCorrection;

    private bool chargingTeleport = false;
    private float teleportTime;
    private Vector3 teleportPosition;

    private float lastJumpHitCount;
    private float lastJumpHitTimeout = Mathf.Infinity;

    private float lastLeftClick = Mathf.NegativeInfinity;
    private RaycastHit lastLeftClickHit;

    public bool ChargingTeleport { get { return chargingTeleport; } }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastLeftClick = Time.time;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out lastLeftClickHit, teleportLayers);
        }

        if (Time.time <= lastLeftClick + leftClickEarlyCorrection && canTeleport && !chargingTeleport)
        {
            if (lastLeftClickHit.collider != null)
            {
                Vector3 target = new Vector3(lastLeftClickHit.point.x, GameInfo.gi.player.transform.position.y, lastLeftClickHit.point.z);
                InitiateTeleport(target);
            }
        }

        if (chargingTeleport && Time.time > teleportTime)
        {
            ExecuteTeleport();
        }

        if (Time.time > lastJumpHitTimeout)
        {
            lastJumpHitCount = 0;
        }
    }

    private void InitiateTeleport(Vector3 target)
    {
        chargingTeleport = true;
        float playbackSpeed = teleportDelay * (1 + lastJumpHitCount);
        float actualDelay = teleportDelay / (1 + lastJumpHitCount);
        teleportTime = Time.time + actualDelay;
        teleportPosition = target;
        GetComponent<PlayerAnimation>().StartTeleportAnimation(playbackSpeed);
        GetComponent<PlayerAnimation>().StartTeleportEndAnimation(target, playbackSpeed);
    }

    private void ExecuteTeleport()
    {
        GameInfo.gi.player.transform.position = teleportPosition;
        chargingTeleport = false;
        lastJumpHitCount = 0;

        foreach (EnemyScript enemy in GameInfo.gi.getEnemiesInRange(teleportPosition, knockoutRange))
        {
            enemy.KnockOut(knockoutDuration);
            lastJumpHitCount++;
        }

        lastJumpHitTimeout = Time.time + jumpHitTimeoutDuration;
    }
}