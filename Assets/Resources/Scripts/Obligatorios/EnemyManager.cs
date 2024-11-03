using UnityEngine;
using System;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static event Action OnAllEnemiesDefeated; // Event to notify when enemies are gone
    private List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy")); // Find all enemies at the start
    }

    public void EnemyDefeated(GameObject enemy)
    {
        enemies.Remove(enemy); // Remove the enemy from the list
        print(enemies.Count);

        if (enemies.Count == 0)
        {
            OnAllEnemiesDefeated?.Invoke(); // Fire event when no enemies remain
        }
    }

    // Public method to manually trigger the event
    public void TriggerAllEnemiesDefeated()
    {
        OnAllEnemiesDefeated?.Invoke();
    }
}
