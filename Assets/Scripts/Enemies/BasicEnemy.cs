using UnityEngine;
using System.Collections;

public class BasicEnemy : EnemyScript
{
    private const float killDistance = 2f;

    void Awake()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        base.Initialize();
    }

    protected override void UpdateAggression()
    {
        if (aggressing || Time.time < aggressionEndTime)
        {
            if (GetComponent<NavMeshAgent>().enabled)
                GetComponent<NavMeshAgent>().destination = GameInfo.gi.player.transform.position;
        }
        else
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }

        if (Vector3.Distance(transform.position, GameInfo.gi.player.transform.position) <= killDistance)
        {
            GameInfo.gi.KillPlayer();
        }
    }

    protected override void Aggress()
    {
        GetComponent<NavMeshAgent>().enabled = true;
        aggressing = true;
    }

    protected override void EndAggress()
    {
        if (aggressionCounter == 0)
        {
            aggressing = false;
            aggressionEndTime = Time.time + aggressionEndDelay;
        }
    }
}
