using UnityEngine;
using UnityEngine.InputSystem;

namespace Bullets
{
    public class BulletSpawner : MonoBehaviour
    {
        [Header("Spawner")]
        [SerializeField] private GameObject spawnPoint;
        [SerializeField] private ObjectPool bulletObjectPool;
        [SerializeField] private ObjectPool missileObjectPool;

        [Header("Input")]
        [SerializeField] private InputActionReference shootAction;
        [SerializeField] private InputActionReference shootMissileAction;

        [SerializeField] private AudioClip bulletSound;
        
        private AudioSource audioSource;

        private void Awake()
        {
            if (bulletObjectPool == null)
            {
                bulletObjectPool = Finder.ObjectPools.Bullet;
            }
            
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
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
                if (bullet == null) return;
                
                bullet.transform.SetPositionAndRotation(spawnPoint.transform.position, spawnPoint.transform.rotation);
                audioSource.PlayOneShot(bulletSound);
            }
            
            if (shootMissileAction.action.triggered)
            {
                var marine = Finder.SpaceMarine;
                if (marine == null || marine.MissilesAmount <= 0) return;
                
                var missile = missileObjectPool.Get();
                missile.transform.SetPositionAndRotation(spawnPoint.transform.position, spawnPoint.transform.rotation);
                
                marine.MissileShot();
            }
        }
    }
}