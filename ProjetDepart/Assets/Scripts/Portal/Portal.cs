using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
