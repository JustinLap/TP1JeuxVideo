using System;
using UnityEngine;

public class Portal : MonoBehaviour, IHurtable
{
    [SerializeField] private int health = 10;
    [SerializeField] private AudioClip portalSound;
    
    private AudioSource audioSource;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnEnable()
    {
        var tracker = FindFirstObjectByType<PortalTracker>();
        tracker?.RegisterPortal(this);
    }

    private void OnDisable()
    {
        var tracker = FindFirstObjectByType<PortalTracker>();
        tracker?.UnregisterPortal(this);
    }

    public void Hurt(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            var spawner = FindFirstObjectByType<CollectibleSpawner>();
            spawner?.SpawnRandomCollectible(transform.position);

            gameObject.SetActive(false);
            
            audioSource.PlayOneShot(portalSound);
        }
    }
}