using System.Collections;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    [SerializeField] private Portal[] spawnPoints;
    [SerializeField] private float spawnDelay = 2f;
    [SerializeField] private int alienLimit = 20;

    private int alienCount = 0;
    private ObjectPool alienPool;

    private void Awake()
    {

        alienPool = Finder.ObjectPools.Alien;
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (alienCount < alienLimit)
            {
                int randomIndex = Random.Range(0, spawnPoints.Length);
                Portal chosenPortal = spawnPoints[randomIndex];

                GameObject alien = alienPool.Get();
                if (alien != null)
                {
                    alien.transform.SetPositionAndRotation(chosenPortal.transform.position,chosenPortal.transform.rotation);

                    alienCount++;
                }
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void OnAlienKilled()
    {
        alienCount--;
    }
}
