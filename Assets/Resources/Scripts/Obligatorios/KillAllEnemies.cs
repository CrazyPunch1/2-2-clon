using UnityEngine;

public class KillAllEnemies : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            KillEnemies();
        }
    }

    void KillEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        Debug.Log("All enemies have been destroyed.");

        // Get a reference to the EnemyManager and trigger the event
        EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
        if (enemyManager != null)
        {
            enemyManager.TriggerAllEnemiesDefeated();
        }
    }
}
