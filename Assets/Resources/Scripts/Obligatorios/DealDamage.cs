using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private string searchedTag;
    [SerializeField] private float dealDmg;
    [SerializeField] private bool destroyOnHit = false;
    [SerializeField] private UnityEvent onHit;
    [SerializeField] private float lifetime = 5f; // Time after which the bullet will disappear

    private void Start()
    {
        // Destroy the bullet after the specified lifetime
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(searchedTag))
        {
            HealthSystem healthSystem = other.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(dealDmg);
            }

            if (destroyOnHit)
            {
                onHit?.Invoke();
                Destroy(gameObject);
            }
        }
    }

    public void Spawn(GameObject go)
    {
        Instantiate(go, transform.parent);
    }
}
