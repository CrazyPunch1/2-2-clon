using UnityEngine;
using UnityEngine.AI;
using System.Collections; // Required for Coroutines

public class EnemyMeleeAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Melee Attack
    public float meleeRange = 2f;        // Range for melee attack
    public int meleeDamage = 10;         // Damage dealt by melee
    public float meleeWindUpTime = 0.5f; // Delay before damage is applied
    public float timeBetweenAttacks = 1.5f; // Cooldown between attacks
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // Stop moving while attacking
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            StartCoroutine(PerformMeleeAttack()); // Start melee attack with timing
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private IEnumerator PerformMeleeAttack()
    {
        Debug.Log("Enemy is winding up for a melee attack...");

        // Trigger melee attack animation (if available)
        Animator animator = GetComponent<Animator>();
        animator?.SetTrigger("MeleeAttack");

        // Wait for the wind-up time before applying damage
        yield return new WaitForSeconds(meleeWindUpTime);
    }

    public void CheckPlayerDamageDealt(){
        
        // Check if player is still within melee range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, meleeRange, whatIsPlayer);
        foreach (Collider hit in hitColliders)
        {
            if (hit.CompareTag("Player"))
            {
                // Apply damage to the player
                HealthSystem playerHealth = hit.GetComponent<HealthSystem>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(meleeDamage);
                    Debug.Log("Player hit by melee attack!");
                }

                // Optional: Play a melee sound effect
                AudioSource audioSource = GetComponent<AudioSource>();
                audioSource?.Play();
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    public void DestroyEnemy()
    {
        OnDeath();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, meleeRange); // Melee range visualization
    }

    private void OnDeath()
    {
        // Call this method when the enemy dies
        EnemyManager manager = FindObjectOfType<EnemyManager>();
        if (manager != null)
        {
            manager.EnemyDefeated(gameObject);
        }

        Destroy(gameObject); // Destroy enemy after death
    }
}
