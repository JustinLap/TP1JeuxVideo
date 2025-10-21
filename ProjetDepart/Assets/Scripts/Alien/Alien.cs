using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour, IHurtable
{
    [Header("Stats")]
    [SerializeField] private int health = 1;
    [SerializeField] private int moveSpeed = 10;
    [SerializeField] private int rotationSpeed = 120;
    [SerializeField] private int damagePlayer = 10;

    private NavMeshAgent agent;
    private SpaceMarine.SpaceMarine spaceMarine;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
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
        if (spaceMarine != null && agent != null)
        {
            agent.SetDestination(spaceMarine.transform.position);
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        var marine = other.gameObject.GetComponent<SpaceMarine.SpaceMarine>();
        if (marine != null)
        {
           // marine.Hurt(damagePlayer);

          //  Die();
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
        var explosion = Finder.ObjectPools.AlienExplosion.Get();
        explosion.transform.position = transform.position;

        // Finder.Audio.Play("AlienExplosion");

        gameObject.SetActive(false);
    }
}
