using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DealDamage : MonoBehaviour
{
    [SerializeField] string searchedTag;
    [SerializeField] float dealDmg;
    [SerializeField] bool destroyOnHit = false;
    [SerializeField] UnityEvent onHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(searchedTag))
        {
            other.GetComponent<HealthSystem>().TakeDamage(dealDmg);

            if (destroyOnHit)
            {
                onHit?.Invoke();
                Destroy(this.gameObject);
            }
        }
    }

    public void Spawn(GameObject go)
    {
        Instantiate(go, this.transform.parent);
    }
}
