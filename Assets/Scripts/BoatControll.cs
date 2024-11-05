using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BoatControll : MonoBehaviour
{
    public float CurrentCooldown = 0f;
    public float MaxCooldown = 20f;
    public Transform turret; 
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;

    public float minAngle = -90f;
    public float maxAngle = 90f;

    public bool isActive = false;
    public ParticleSystem gunparticle;
    public float holdtimer = 0;
    public float bowHoldTime = 2;
    public cameraScript CameraScript;
    
    void Update()
    {
        if (isActive)
        {
            if (CurrentCooldown > 0)
            {
                CurrentCooldown -= Time.deltaTime;
            }

            AimTurret();

            print(CurrentCooldown);

            if (Input.GetKey(KeyCode.Mouse1)) // Shooting starts
            {
                holdtimer += Time.fixedDeltaTime;
                holdtimer = Mathf.Clamp(holdtimer, 0, bowHoldTime);
            }
            else // Mouse1 button is not held
            {
                if (holdtimer > 1.8f)
                {
                    CameraScript.StartShake();
                    gunparticle.Play();
                    GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, -90));
                    Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                    rb.velocity = firePoint.right * projectileSpeed;
                    holdtimer = 0;
                }
                else
                {
                    holdtimer = 0;
                }
            }
        }
    }

    void AimTurret()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Calculate the angle to look at
        Vector3 aimDirection = mousePos - turret.position;
        float targetAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        targetAngle = Mathf.Clamp(targetAngle, minAngle, maxAngle);

        // Smoothly rotate the turret
        float rotationSpeed = 5f; // Adjust this speed as desired
        float currentAngle = turret.localRotation.eulerAngles.z;
        float smoothAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotationSpeed);

        turret.localRotation = Quaternion.Euler(0, 0, smoothAngle);
    }
}
