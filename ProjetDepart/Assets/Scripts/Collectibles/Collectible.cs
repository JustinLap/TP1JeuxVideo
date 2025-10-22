using System.Collections;
using UnityEngine;
using SpaceMarine;

namespace Collectibles
{
    public class Collectible : MonoBehaviour
    {
        [SerializeField] private AudioClip collectibleSound;
        
        private Rigidbody rigidbody;
        
        private AudioSource audioSource;

        public enum CollectibleType { Health, Missile, Armor }
        public CollectibleType Type { get; private set; }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        public void Initialize(CollectibleType type)
        {
            Type = type;
            gameObject.SetActive(true);

            StartCoroutine(AutoDisable());
        }

        private IEnumerator AutoDisable()
        {
            yield return new WaitForSeconds(10f);
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<SpaceMarine.SpaceMarine>();
            if (player == null) return;

            ApplyEffect(player);
            gameObject.SetActive(false);
            
            audioSource.PlayOneShot(collectibleSound);
        }

        private void ApplyEffect(SpaceMarine.SpaceMarine player)
        {
            switch (Type)
            {
                case CollectibleType.Health:
                    player.Heal();
                    break;
                case CollectibleType.Missile:
                    player.AddMissiles();
                    break;
                case CollectibleType.Armor:
                    player.AddArmor();
                    break;
            }
        }
    }
}