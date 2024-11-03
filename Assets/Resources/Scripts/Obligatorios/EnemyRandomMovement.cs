using UnityEngine;
using UnityEngine.AI;

public class EnemyRandomMovement : MonoBehaviour
{
    public float maxDistance = 5f; // Maximum distance from the initial position
    public float moveInterval = 2f; // Time interval for movement
    private Vector3 initialPosition;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        initialPosition = transform.position; // Store the initial position
        navMeshAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component

        // Start the movement coroutine
        InvokeRepeating(nameof(MoveToRandomPosition), 0, moveInterval);
    }

    void MoveToRandomPosition()
    {
        // Generate a random point within a sphere defined by maxDistance
        Vector3 randomDirection = Random.insideUnitSphere * maxDistance;

        // Calculate the new destination
        Vector3 newDestination = initialPosition + randomDirection;

        // Check if the destination is on the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(newDestination, out hit, maxDistance, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position); // Set the destination to the hit position
        }
    }
}
