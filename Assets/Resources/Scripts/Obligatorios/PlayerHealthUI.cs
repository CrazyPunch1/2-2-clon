using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public HealthSystem health; // Reference to the PlayerHealth script
    public Image healthBarFill; // Reference to the health bar fill image

    void Update()
    {
        // Update the health bar fill amount based on the player's current health
        healthBarFill.fillAmount = health.currentHealth / health.maxHealth;
    }
}
