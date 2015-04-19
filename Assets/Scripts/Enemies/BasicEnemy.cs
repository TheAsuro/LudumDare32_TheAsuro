using UnityEngine;
using System.Collections;

public class BasicEnemy : EnemyScript
{
    private const float killDistance = 2f;

    protected override void Initialize()
    {
        GetComponent<NavMeshAgent>().enabled = false;
    }

    protected override void UpdateAggression()
    {
        if (aggressing)
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
        }
    }

    protected override void ResetEnemy()
    {
        GetComponent<NavMeshAgent>().enabled = false;
    }
}
