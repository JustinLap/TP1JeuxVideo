using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceMarine
{
    public class SpaceMarine : MonoBehaviour, IHurtable
    {
        [Header("Movement")]
        [SerializeField] private float speed = 25f;
        [SerializeField] private float rotationSpeed = 30f;
        [SerializeField] private float jumpHeight = 10f;
        
        [Header("Life points")]
        [SerializeField] private int healthPoints = 25;
        [SerializeField] private int startLifePoints = 50;
        [SerializeField] private int maxLifePoints = 100;
        [SerializeField] private int lifePoints;
        [SerializeField] private bool dead;
        
        public int LifePoints => lifePoints;

        [Header("Munitions")]
        [SerializeField] private int missilesAmount;
        [SerializeField] private int missilesMax = 10;
        
        public int MissilesAmount => missilesAmount;
        
        [Header("Invulnerability")]
        [SerializeField] private float armorDuration = 5f;
        [SerializeField] private float invulnerabilityDuration = 1.5f;
        [SerializeField] private float invulnerabilityTime;
        [SerializeField] private bool invulnerable;
        
        [Header("Inputs")]
        [SerializeField] private InputActionReference moveAction;
        [SerializeField] private InputActionReference jumpAction;
        
        [Header("Audio")]
        [SerializeField] private AudioClip deathSound;
        [SerializeField] private AudioClip hurtSound;
        private AudioSource audioSource;
        
        private CharacterController characterController;
        private float verticalVelocity;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            characterController = GetComponent<CharacterController>();
            
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        private void Start()
        {
            lifePoints = startLifePoints;
            dead = false;
            invulnerable = false;
            missilesAmount = 0;
        }

        private void Update()
        {
            var camera = Camera.main!;
            var cameraTransform = camera.transform;
            var up = transform.up;
            var forward = cameraTransform.forward;
            var right = cameraTransform.right;
            
            forward.y = 0;
            right.y = 0;
            
            if (!dead && lifePoints <= 0)
            {
                dead = true;
                Finder.EventChannels.PublishLevelLose();
                audioSource.PlayOneShot(deathSound);
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
            
            if (invulnerable)
            {
                invulnerabilityTime -= Time.deltaTime;
                if (invulnerabilityTime <= 0f)
                {
                    invulnerable = false;
                    invulnerabilityTime = 0f;
                }
            }
        }

        public void MissileShot()
        {
            missilesAmount--;
        }

        public void Hurt(int damage)
        {
            if (invulnerable) return;
            
            lifePoints -= damage;
            
            invulnerable = true;
            invulnerabilityTime = invulnerabilityDuration;
            
            audioSource.PlayOneShot(hurtSound);
        }

        public void Heal()
        {
            lifePoints += healthPoints;
            if (lifePoints > maxLifePoints)
            {
                lifePoints = maxLifePoints;
            }
        }

        public void AddMissiles()
        {
            missilesAmount += 5;
            if (missilesAmount >= missilesMax)
            {
                missilesAmount = missilesMax;
            }
        }

        public void AddArmor()
        {
            invulnerable = true;
            invulnerabilityTime = armorDuration;
        }
    }
}