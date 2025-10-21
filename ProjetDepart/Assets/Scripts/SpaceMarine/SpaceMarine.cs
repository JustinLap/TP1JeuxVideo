using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceMarine
{
    public class SpaceMarine : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float speed = 25f;
        [SerializeField] private float rotationSpeed = 30f;
        [SerializeField] private float jumpHeight = 10f;
        
        [Header("Life points")]
        [SerializeField] private int startLifePoints = 50;
        //[SerializeField] private int maxLifePoints = 100;
        [SerializeField] private int lifePoints;
        [SerializeField] private bool dead;
        
        [Header("Invulnerability")]
        [SerializeField] private float invulnerabilityTime;
        [SerializeField] private bool invulnerable;
        
        [Header("Inputs")]
        [SerializeField] private InputActionReference moveAction;
        [SerializeField] private InputActionReference jumpAction;
        
        private CharacterController characterController;
        private float verticalVelocity;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            lifePoints = startLifePoints;
            dead = false;
            invulnerable = false;
        }

        private void Update()
        {
            var camera = Camera.main!;
            var cameraTransform = camera.transform;
            var up = transform.up;
            var forward = cameraTransform.forward;
            var right = cameraTransform.right;
            
            if (lifePoints <= 0)
            {
                dead = true;
            }
            
            // Lire les entrées du joueur.
            var moveInput = moveAction.action.ReadValue<Vector2>();
            var jumpInput = jumpAction.action.triggered;

            var horizontalMovement = Vector3.zero;
        
            // Si le joueur ne veut pas bouger, ne pas faire bouger le joueur.
            if (moveInput != Vector2.zero)
            {
                // Y multiplie forward (avance/recule).
                // X multiplie right (gauche/droite).
                // Combinaison des deux fait le mouvement total.
                var moveDirection = forward * moveInput.y + right * moveInput.x;
                horizontalMovement = moveDirection * (speed * Time.deltaTime);
            
                // Rotate player using current direction.
                var lookRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            }
            
            // Partie sur le saut.
            var gravity = Physics.gravity;
            var isGrounded = characterController.isGrounded;
        
        
            // La vélocité est de zéro si on touche le sol.
            if (isGrounded)
            {
                verticalVelocity = 0;
            }
        
            // Calculer la vélocité lorsque l'on saute.
            //
            // Vélocité * vélocité = 2 x acceleration (gravité) x déplacememt (hauteur voulue)
            if (isGrounded && jumpInput)
            {
                verticalVelocity = Mathf.Sqrt(2 * -gravity.y * jumpHeight);
            }
        
            // Appliquer la gravité.
            verticalVelocity += gravity.y * Time.deltaTime;
        
            // Calculer le mouvement vertical.
            var verticalMovement = up * (verticalVelocity * Time.deltaTime);
        
            // Appliquer le mouvement.
            characterController.Move(horizontalMovement + verticalMovement);
            
            if (IsInvulnerable())
            {
                invulnerabilityTime -= Time.deltaTime;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            //var alien = other.gameObject.GetComponent<Alien>();
            if (!IsInvulnerable() /*&& alien*/)
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