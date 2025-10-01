using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceMarine
{
    public class SpaceMarine : MonoBehaviour
    {
        
        [Header("Life points")]
        [SerializeField] private int startLifePoints = 50;
        [SerializeField] private int maxLifePoints = 100;
        [SerializeField] private int lifePoints;
        [SerializeField] private bool dead = false;
        
        [Header("Invulnerability")]
        [SerializeField] private float invulnerabilityTime;
        [SerializeField] private bool invulnerable = false;
        
        [Header("Inputs")]
        [SerializeField] private InputActionReference moveAction;
        [SerializeField] private InputActionReference jumpAction;
        
        private CharacterController characterController;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (lifePoints <= 0)
            {
                dead = true;
            }
            
            var moveInput = moveAction.action.ReadValue<Vector2>();
            var jumpInput = jumpAction.action.triggered;
            
            if (IsInvulnerable())
            {
                invulnerabilityTime -= Time.deltaTime;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!IsInvulnerable())
            {
                lifePoints -= 10;
                invulnerable = true;
            }
        }

        private bool IsInvulnerable()
        {
            if (invulnerabilityTime <= 0)
            {
                invulnerabilityTime = 1.5f;
                invulnerable = false;
            }
            else
            {
                invulnerable = true;
            }

            return invulnerable;
        }

        public bool IsDead()
        {
            return dead;
        }
    }
}