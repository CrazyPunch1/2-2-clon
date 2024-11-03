using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneToLoad; // Scene name to load
    public GameObject door;    // Door game object to activate

    private void Start()
    {
        door.SetActive(false); // Initially hide the door
        EnemyManager.OnAllEnemiesDefeated += ActivateDoor; // Subscribe to the event
    }

    private void OnDestroy()
    {
        EnemyManager.OnAllEnemiesDefeated -= ActivateDoor; // Unsubscribe to avoid memory leaks
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad); // Load the specified scene
        }
    }

    private void ActivateDoor()
    {
        door.SetActive(true); // Show the door when all enemies are defeated
    }
}
