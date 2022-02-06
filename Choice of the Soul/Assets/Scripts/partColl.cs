using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class partColl : MonoBehaviour
{
    ParticleSystem particle;
    public GameObject splatPrefab;
    public Transform splatHolder;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    public AudioSource audioSource;
    public AudioClip[] sounds;
    public float soundCapResetSpeed = 0.55f;
    public int maxSounds = 3;
    float timePassed;
    int soundsPlayed;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > soundCapResetSpeed)
        {
            soundsPlayed = 0;
            timePassed = 0;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particle, other, collisionEvents);

        int count = collisionEvents.Count;

        for (int i = 0; i < count; i++)
        {
            Instantiate(splatPrefab, collisionEvents[i].intersection, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)), splatHolder);
            if (soundsPlayed < maxSounds)
            {
                soundsPlayed += 1;
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)], Random.Range(0.1f, 0.35f));
            }
        }
    }
}