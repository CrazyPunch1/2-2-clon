using UnityEngine;
using Cinemachine;


public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab; // The projectile to instantiate
    [SerializeField] Transform firePoint; // The point from where the projectile is shot
    [SerializeField] float projectileSpeed = 20f; // Speed of the projectile
    [SerializeField] float fireRate = 0.5f; // Time between shots
    private float nextFireTime = 0f;
    [SerializeField] float bulletLife = 1;



    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Create a new projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody component of the projectile and apply force to it
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * projectileSpeed;

        Destroy(projectile, bulletLife);
    }


}
