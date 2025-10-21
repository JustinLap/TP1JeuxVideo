using UnityEngine;

namespace Bullets
{
    public class Missile : MonoBehaviour
    {
        [Header("Missile Settings")]
        [SerializeField] private float speed = 200f;
        [SerializeField] private int damage = 10;
        [SerializeField] private float explosionRadius = 20f;
        [SerializeField] private float lifetime = 5f;

        private Rigidbody rb;
        private float deathTime;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            deathTime = Time.time + lifetime;
        }

        private void Update()
        {
            rb.linearVelocity = transform.forward * speed;

            if (Time.time >= deathTime)
                Explode();
        }

        private void OnTriggerEnter(Collider other)
        {
            Explode();
        }

        private void Explode()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (var hit in hits)
            {
                Alien alien = hit.GetComponent<Alien>();
                if (alien != null)
                {
                    continue;
                }

                Portal portal = hit.GetComponent<Portal>();
                if (portal != null)
                {
                    //portal.Hit();
                }
            }

            gameObject.SetActive(false);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}
