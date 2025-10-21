using UnityEngine;
using UnityEngine.InputSystem;

namespace Bullets
{
    public class BulletSpawner : MonoBehaviour
    {
        [Header("Spawner")]
        [SerializeField] private GameObject spawnPoint;
        [SerializeField] private ObjectPool bulletObjectPool;

        [Header("Input")]
        [SerializeField] private InputActionReference shootAction;

        private void Awake()
        {
            if (bulletObjectPool == null)
            {
                bulletObjectPool = Finder.ObjectPools.Bullet;
            }
        }
        
        private void Update()
        {
            UpdateSpawning();
        }
        
        private void UpdateSpawning()
        {
            if (shootAction.action.triggered)
            {
                var bullet = bulletObjectPool.Get();
            
                bullet.transform.position = spawnPoint.transform.position;
                bullet.transform.rotation = spawnPoint.transform.rotation;
            }
        }
    }
}