using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 30;     // Amount of ammo restored
    [SerializeField] private AudioClip pickupSound;   // Optional sound on pickup
    [SerializeField] private GameObject pickupEffect; // Optional visual effect
    [SerializeField] private float destroyDelay = 0.5f; // Delay before destroying the box

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("AmmoBox triggered by Player");

            // Check if the player has a GunInventory component
            GunInventory GI = other.GetComponent<GunInventory>();

            if (GI != null && GI.currentGun != null)
            {
                Debug.Log("Player collided with AmmoBox. GunInventory found.");

                GunScript gunScript = GI.currentGun.GetComponent<GunScript>();
                if (gunScript != null && !gunScript.IsAmmoFull())
                {
                    Debug.Log("GunScript found on current gun.");

                    if (!gunScript.IsAmmoFull())
                    {
                        Debug.Log("Adding ammo: " + ammoAmount);

                        // Refill the player's ammo
                        gunScript.AddAmmo(ammoAmount);

                        // Optional: Play a sound effect
                        if (pickupSound != null)
                            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

                        // Optional: Instantiate a visual effect
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
                    Debug.LogWarning("No GunScript found on current gun.");
                }
            }
            else
            {
                Debug.LogWarning("No GunInventory found on colliding object.");
            }
        }
    }
}
