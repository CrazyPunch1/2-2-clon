using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f; // Max health for both player and enemies
    public float currentHealth;
    [SerializeField] private float timeToSelfDestructOnDeath = 1;
    [SerializeField] private UnityEvent onBeenHit;
    [SerializeField] private UnityEvent onDeath;
    

    void Start()
    {
        currentHealth = maxHealth; // Set current health to max at the start
    }

    // Method to apply damage to this object
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            onDeath?.Invoke();
        }
        else
        {
            onBeenHit?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
    }

    // Optional: Method to check if the object is alive
    public bool IsAlive()
    {
        return currentHealth > 0f;
    }
}
