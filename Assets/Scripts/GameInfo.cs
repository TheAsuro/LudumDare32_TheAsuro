using UnityEngine;
using System.Collections.Generic;

public class GameInfo : MonoBehaviour
{
    public static GameInfo gi;

    public GameObject player;
    public LayerMask blockLayer;

    private Dictionary<EnemyScript, Transform> enemies = new Dictionary<EnemyScript, Transform>();
    private List<EnemyTrigger> allTriggers = new List<EnemyTrigger>();
    private List<IResetObject> resetObjects = new List<IResetObject>();
    private bool playerHasTarget = false;
    private bool paused = false;

    public bool Paused { get { return paused; } }

    void Awake()
    {
        if (gi == null)
            gi = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void OnLevelWasLoaded(int level)
    {
        ResumeGame();
        GetComponent<GameMenu>().SetMenuState(GameMenu.MenuState.NoMenu);
        enemies.Clear();
        allTriggers.Clear();
        resetObjects.Clear();
    }

    public void AddEnemy(EnemyScript newEnemy, Transform enemyTf)
    {
        enemies.Add(newEnemy, enemyTf);
    }

    public void AddTrigger(EnemyTrigger trigger)
    {
        allTriggers.Add(trigger);
    }

    public void AddResetObject(IResetObject obj)
    {
        resetObjects.Add(obj);
    }

    public void RemoveResetObject(IResetObject obj)
    {
        if (resetObjects.Contains(obj))
            resetObjects.Remove(obj);
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
            PauseGame();
            GetComponent<GameMenu>().SetMenuState(GameMenu.MenuState.EndMenu);
        }
    }

    public void KillPlayer()
    {
        ResetGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        paused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        paused = false;
    }

    public void Restart()
    {
        GetComponent<GameMenu>().SetMenuState(GameMenu.MenuState.NoMenu);
        ResetGame();
        ResumeGame();
    }

    private void ResetGame()
    {
        player.GetComponent<PlayerMovement>().ResetPlayer();
        foreach (EnemyScript enemy in enemies.Keys)
        {
            enemy.Reset();
        }
        resetObjects.ForEach((IResetObject obj) => obj.Reset());

        playerHasTarget = false;
    }

    public void LoadNextLevel()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    public void SetMenuState(GameMenu.MenuState state)
    {
        GetComponent<GameMenu>().SetMenuState(state);
    }

    public bool InMenu { get { return GetComponent<GameMenu>().GetMenuState() == GameMenu.MenuState.NoMenu; } }
}

public interface IResetObject
{
    void Reset();
}