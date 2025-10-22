using UnityEngine;

namespace Bullets
{
    public class Missile : MonoBehaviour
    {
        [Header("Missile Settings")]
        [SerializeField] private float speed = 200f;
        [SerializeField] private int damage = 10;
        [SerializeField] private float explosionRadius = 20f;
        [SerializeField] private AudioClip explosion;

        private new Rigidbody rigidbody;
        private float deathTime;
        
        private AudioSource audioSource;
        
        [Header("Pools")]
        private ObjectPool missileObjectPool;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            missileObjectPool = Finder.ObjectPools.Missile;
            
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        private void Update()
        {
            var forward = rigidbody.transform.forward;
            rigidbody.linearVelocity = forward * speed;
        }

        private void OnEnable()
        {
            rigidbody.linearVelocity = rigidbody.transform.forward * speed;
            rigidbody.angularVelocity = Vector3.zero;
        }

        private void OnTriggerEnter(Collider other)
        {
            Explode(other);
            
            audioSource.PlayOneShot(explosion);
            missileObjectPool.Release(gameObject);
        }

        private void Explode(Collider other)
        {
            Collider[] hits = Physics.OverlapSphere(rigidbody.transform.position, explosionRadius);

            foreach (var hit in hits)
            {
                var alien = hit.GetComponent<Alien>();
                if (alien != null)
                {
                    alien.Hurt(damage);
                    continue;
                }

                var portal = hit.GetComponent<Portal>();
                if (portal != null)
                {
                    portal.Hurt(damage);
                }
            }
        }
    }
}
