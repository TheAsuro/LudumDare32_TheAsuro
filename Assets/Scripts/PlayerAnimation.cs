using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{
    public GameObject teleportParticleTemplate;
    public GameObject teleportEndTemplate;
    public float defaultTeleportDelay = 2f;
   
    public void StartTeleportAnimation(float teleportDelay)
    {
        GameObject instance = GameObject.Instantiate<GameObject>(teleportParticleTemplate);
        instance.transform.position = this.transform.position + new Vector3(0f, -1f, 0f);

        foreach (ParticleSystem ps in instance.transform.GetComponentsInChildren<ParticleSystem>())
        {
            float delaySpeed = teleportDelay / defaultTeleportDelay;
            ps.playbackSpeed = delaySpeed;
        }
    }

    public void StartTeleportEndAnimation(Vector3 endPos, float teleportDelay)
    {
        GameObject instance = GameObject.Instantiate<GameObject>(teleportEndTemplate);
        instance.transform.position = endPos + new Vector3(0f, -1f, 0f);

        foreach (ParticleSystem ps in instance.transform.GetComponentsInChildren<ParticleSystem>())
        {
            float delaySpeed = teleportDelay / defaultTeleportDelay;
            ps.playbackSpeed = delaySpeed;
        }
    }
}
