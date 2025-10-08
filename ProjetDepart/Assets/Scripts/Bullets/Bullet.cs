using System;
using UnityEngine;

namespace Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 200f;
        
        private new Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
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
/*
        private void OnTriggerEnter(Collider other)
        {
            var alien = other.GetComponent<Alien>();
            if (alien == null) return;
        }*/
    }
}