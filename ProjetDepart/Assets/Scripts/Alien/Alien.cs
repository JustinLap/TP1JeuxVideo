using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour
{
    [SerializeField] private int moveSpeed = 10;
    [SerializeField] private int rotationSpeed = 120;
    // [SerializeField] private int damagePlayer = 10;

    private NavMeshAgent agent;
    private SpaceMarine.SpaceMarine spaceMarine;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if (agent != null)
        {
            agent.speed = moveSpeed;
            agent.angularSpeed = rotationSpeed;
        }

        spaceMarine = FindAnyObjectByType<SpaceMarine.SpaceMarine>();
    }

    void Update()
    {
        if (spaceMarine == null || agent == null || !agent.isOnNavMesh) return;
        
        agent.SetDestination(spaceMarine.transform.position);
    }

    //Dommages (� compl�ter)
    void OnCollisionEnter(Collision other)
    {
        var marine = other.gameObject.GetComponent<SpaceMarine.SpaceMarine>();
        if (marine != null)
        {

        }
    }
    
}
