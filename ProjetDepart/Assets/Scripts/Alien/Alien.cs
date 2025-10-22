using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour, IHurtable
{
    [Header("Stats")]
    [SerializeField] private int health = 1;
    [SerializeField] private int moveSpeed = 10;
    [SerializeField] private int rotationSpeed = 120;
    [SerializeField] private int damagePlayer = 10;

    [SerializeField] private AudioClip alienExplosion;

    private NavMeshAgent agent;
    private SpaceMarine.SpaceMarine spaceMarine;
    
    private ObjectPool objectPool;
    
    private AudioSource audioSource;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        objectPool = Finder.ObjectPools.Alien;
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        if (agent != null)
        {
            agent.speed = moveSpeed;
            agent.angularSpeed = rotationSpeed;
        }

        spaceMarine = FindAnyObjectByType<SpaceMarine.SpaceMarine>();
    }

    private void Update()
    {
        if (spaceMarine == null || agent == null || !agent.isOnNavMesh) return;
        
        agent.SetDestination(spaceMarine.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        var marine = other.GetComponent<SpaceMarine.SpaceMarine>();
        if (marine != null)
        {
            marine.Hurt(damagePlayer);

            Die();
        }
    }

    public void Hurt(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (agent != null)
        {
            agent.enabled = false;
        }

        var alienSpawner = FindAnyObjectByType<AlienSpawner>();
        if (alienSpawner != null)
        {
            alienSpawner.OnAlienKilled();
        }

        objectPool.Release(gameObject);
    }
}