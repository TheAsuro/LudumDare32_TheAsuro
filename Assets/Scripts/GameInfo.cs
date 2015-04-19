using UnityEngine;
using System.Collections.Generic;

public class GameInfo : MonoBehaviour
{
    public static GameInfo gi;

    public GameObject player;
    public LayerMask defaultLayer;

    private Dictionary<EnemyScript, Transform> enemies = new Dictionary<EnemyScript, Transform>();
    private List<EnemyTrigger> allTriggers = new List<EnemyTrigger>();
    private bool playerHasTarget = false;

    void Awake()
    {
        if (gi == null)
            gi = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void AddEnemy(EnemyScript newEnemy, Transform enemyTf)
    {
        enemies.Add(newEnemy, enemyTf);
    }

    public void AddTrigger(EnemyTrigger trigger)
    {
        allTriggers.Add(trigger);
    }

    public void UpdateTriggers()
    {
        allTriggers.ForEach((EnemyTrigger trigger) => trigger.UpdatePlayerCollision());
    }

    public List<EnemyScript> getEnemiesInRange(Vector3 centerPos, float range)
    {
        List<EnemyScript> returnList = new List<EnemyScript>();

        foreach (KeyValuePair<EnemyScript, Transform> pair in enemies)
        {
            if (!pair.Key.KnockedOut && Vector3.Distance(pair.Value.position, centerPos) <= range)
            {
                returnList.Add(pair.Key);
            }
        }

        return returnList;
    }

    public void TargetCollected()
    {
        playerHasTarget = true;
    }

    public void GoalEntered()
    {
        if (playerHasTarget)
        {
            print("YOU WIN!");
        }
    }

    public void KillPlayer()
    {
        print("YOU DIED!");
    }
}
