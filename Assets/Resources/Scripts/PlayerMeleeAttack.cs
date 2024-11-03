using UnityEngine;
using System.Collections;

public class PlayerMeleeAttack : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public float attackDuration = 0.5f; // Duration of the melee attack
    public float attackCooldown = 1.0f; // Cooldown time between attacks
    public bool isAttacking = false; // Check if player is currently attacking
    public bool canAttack = true; // Check if player can attack (cooldown)
    public int damageAmount = 100; // Amount of damage dealt to enemies
    public LayerMask Enemy; // Layer to identify enemies

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isAttacking && canAttack)
        {
            StartCoroutine(PerformMeleeAttack());
        }
    }

    IEnumerator PerformMeleeAttack()
    {
        canAttack = false; // Prevent further attacks during cooldown
        isAttacking = true; // Player is now attacking

        // Trigger the melee attack animation
        animator.SetTrigger("MeleeAttack");

        // Check for hits (using a simple overlap sphere for demonstration)
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, 1f, Enemy);
        foreach (Collider enemy in hitEnemies)
        {
            EnemyAiTutorial enemyComponent = enemy.GetComponent<EnemyAiTutorial>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(damageAmount); // Deal damage to the enemy
            }
        }

        // Wait for the duration of the attack
        yield return new WaitForSeconds(attackDuration);

        isAttacking = false; // Player is no longer attacking

        // Cooldown before the player can attack again
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the attack range in the editor (optional)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f); // Adjust radius as necessary
    }
}
