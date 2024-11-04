using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 30;     // Amount of ammo restored
    [SerializeField] private AudioClip pickupSound;   // Optional sound on pickup
    [SerializeField] private GameObject pickupEffect; // Optional visual effect
    [SerializeField] private float destroyDelay = 0.5f; // Delay before destroying the box

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("AmmoBox triggered by Player");

            // Retrieve the GunInventory component from the player
            GunInventory gunInventory = other.GetComponent<GunInventory>();

            if (gunInventory != null && gunInventory.currentGun != null)
            {
                Debug.Log("Player has GunInventory and current gun equipped.");

                // Retrieve the GunScript component from the current gun
                GunScript gunScript = gunInventory.currentGun.GetComponent<GunScript>();

                if (gunScript != null)
                {
                    if (!gunScript.IsAmmoFull())
                    {
                        Debug.Log("Adding ammo: " + ammoAmount);

                        // Refill the player's ammo
                        gunScript.AddAmmo(ammoAmount);

                        // Play a pickup sound if assigned
                        if (pickupSound != null)
                            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

                        // Instantiate a visual effect if assigned
                        if (pickupEffect != null)
                            Instantiate(pickupEffect, transform.position, Quaternion.identity);

                        // Destroy the ammo box after a short delay
                        Destroy(gameObject, destroyDelay);
                    }
                    else
                    {
                        Debug.Log("Ammo is already full.");
                    }
                }
                else
                {
                    Debug.LogWarning("No GunScript found on the current gun.");
                }
            }
            else
            {
                Debug.LogWarning("No GunInventory or current gun found on the player.");
            }
        }
    }
}
