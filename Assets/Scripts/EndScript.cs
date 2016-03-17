using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndScript : MonoBehaviour
{
    public float displayDuration;

    float endTime;

    void Awake()
    {
        endTime = Time.time + displayDuration;
    }

    void Update()
    {
        if (Time.time > endTime)
        {
            SceneManager.LoadScene(0);
        }
    }
}
