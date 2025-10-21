using UnityEngine;

public class Portal : MonoBehaviour, IHurtable
{
    [SerializeField] private int health = 10;

    private void OnEnable()
    {
        var tracker = FindFirstObjectByType<PortalTracker>();
        tracker?.RegisterPortal(this);
    }

    private void OnDisable()
    {
        var tracker = FindFirstObjectByType<PortalTracker>();
        tracker?.UnregisterPortal(this);
    }

    public void Hurt(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}