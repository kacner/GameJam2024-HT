using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatControll : MonoBehaviour
{
    public float CurrentCooldown = 0f;
    public float MaxCooldown = 20f;
    public Transform turret; // Reference to the turret that rotates
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform firePoint; // Where the projectile will spawn
    public float projectileSpeed = 10f;

    public float minAngle = -90f;
    public float maxAngle = 90f; 
    
    void Update()
    {
        // Decrease cooldown if greater than zero
        if (CurrentCooldown > 0)
        {
            CurrentCooldown -= Time.deltaTime;
        }

        AimTurret();

        // Check if cooldown is zero or below, then allow shooting
        if (Input.GetButtonDown("Fire1") && CurrentCooldown <= 0f)
        {
            Shoot();
            CurrentCooldown = MaxCooldown; // Reset cooldown
        }

        print(CurrentCooldown);
    }


    void AimTurret()
    {
        // Get the mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Make sure we're in 2D space

        // Calculate the direction to aim and set the turret's rotation
        Vector3 aimDirection = mousePos - turret.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        turret.localRotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {

        // Instantiate the projectile and set its velocity in the firing direction
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * projectileSpeed;
    }
}
