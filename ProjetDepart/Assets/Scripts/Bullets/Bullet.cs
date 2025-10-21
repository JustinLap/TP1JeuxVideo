using System;
using System.Runtime.ConstrainedExecution;
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
        private ObjectPool alienObjectPool;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            bulletObjectPool = Finder.ObjectPools.Bullet;
            alienObjectPool = Finder.ObjectPools.Alien;
        }

        private void Update()
        {
            var forward = rigidbody.transform.forward;
            rigidbody.linearVelocity = forward * speed;
        }

        private void OnEnable()
        {
            rigidbody.position = Vector3.zero;
            rigidbody.rotation = Quaternion.identity;
            rigidbody.linearVelocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        private void OnTriggerEnter(Collider other)
        {
            bulletObjectPool.Release(gameObject);
            
            var alien = other.GetComponent<Alien>();
            if (alien == null) return;
            
            alienObjectPool.Release(alien.gameObject);
            
            var alienSpawner = FindAnyObjectByType<AlienSpawner>();
            if (alienSpawner == null) return;
            alienSpawner.OnAlienKilled();
            
            var portal = other.GetComponent<Portal>();
            if (portal == null) return;
            portal.Hurt(damage);
        }
    }
}