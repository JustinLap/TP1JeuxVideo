using UnityEngine;

namespace Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 200f;
        [SerializeField] private int damage = 1;
        
        private new Rigidbody rigidbody;

        [Header("Pools")]
        private ObjectPool bulletObjectPool;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            bulletObjectPool = Finder.ObjectPools.Bullet;
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
            bulletObjectPool.Release(gameObject);
            
            var alien = other.GetComponent<Alien>();
            if (alien != null)
            {
                alien.Hurt(damage);
                return;
            }
            
            var portal = other.GetComponent<Portal>();
            if (portal == null) return;
            portal.Hurt(damage);
        }
    }
}