using UnityEngine;
using System.Collections;

public class ParticleSelfDestruct : MonoBehaviour, IResetObject
{
    float deathTime = Mathf.Infinity;

    void Awake()
    {
        float maxParticleRunTime = 0f;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            ParticleSystem system = obj.GetComponent<ParticleSystem>();
            
            if (system != null)
            {
                maxParticleRunTime = Mathf.Max(maxParticleRunTime, system.duration);
            }
        }

        if (GetComponent<ParticleSystem>() != null)
            maxParticleRunTime = Mathf.Max(maxParticleRunTime, GetComponent<ParticleSystem>().duration);

        deathTime = Time.time + maxParticleRunTime;
    }

    void Start()
    {
        GameInfo.gi.AddResetObject(this);
    }

    void Update()
    {
        if (Time.time > deathTime)
        {
            Reset();
        }
    }

    public void Reset()
    {
        GameInfo.gi.RemoveResetObject(this);
        GameObject.Destroy(gameObject);
    }

    void IResetObject.Reset()
    {
        Reset();
    }
}
