using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float healAmount = 25f; // Amount of health restored
    [SerializeField] private AudioClip pickupSound;  // Optional: Sound on pickup
    [SerializeField] private GameObject pickupEffect; // Optional: Visual effect on pickup
    [SerializeField] private float destroyDelay = 0.5f; // Time to destroy the pickup after use

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has a HealthSystem component
        HealthSystem healthSystem = other.GetComponent<HealthSystem>();
        if (healthSystem != null && healthSystem.IsAlive())
        {
            // Heal the player
            healthSystem.Heal(healAmount);

            // Optional: Play a pickup sound
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            // Optional: Instantiate visual effect
            if (pickupEffect != null)
                Instantiate(pickupEffect, transform.position, Quaternion.identity);

            // Destroy the pickup after a short delay
            Destroy(gameObject, destroyDelay);
        }
    }
}
