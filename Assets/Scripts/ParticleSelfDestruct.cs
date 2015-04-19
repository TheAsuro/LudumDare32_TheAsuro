using UnityEngine;
using System.Collections;

public class ParticleSelfDestruct : MonoBehaviour
{
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

        GameObject.Destroy(gameObject, maxParticleRunTime);
    }
}
