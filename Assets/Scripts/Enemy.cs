using System;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public GameObject Wall;
    public float speed = 3;
    public GameObject projectilePrefab;
    public Transform Player;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float shootingcooldown = 1f;
    public float cooldownTimer = 0f;
    public float distanceToStop;
    public float accelerationTime = 2f;
    public float maxSpeed = 5f;
    private Vector2 movement;
    private float timeLeft;

    private Camera mainCamera;
    private float distance;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    private void Start()
    {
        float direction = UnityEngine.Random.Range(0, 2) == 0 ? -5f : 5f;
        movement = new Vector2(direction, 0).normalized;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootAtPlayer();
        AimAtPlayer();

        if (timeLeft <= 0)
        {
            ChangeDirection();
            timeLeft = accelerationTime;
        }
        distance = Vector2.Distance(transform.position, Wall.transform.position);
        Vector2 direction = (Wall.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceToStop)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Wall.transform.position, speed * Time.deltaTime);
            Vector2 direction34 = (Wall.transform.position - transform.position).normalized;
            if (rb != null)
                rb.AddForce(direction34 * speed * Time.deltaTime, ForceMode2D.Force);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        else
        {
            rb.velocity = Vector2.zero;
            Debug.Log("stop");

            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                float randomDirection = UnityEngine.Random.Range(-5f, 5f) >= 0 ? 5f : -5f;

                Vector3 screenDirection = new Vector3(randomDirection, 0, Camera.main.nearClipPlane);
                Vector3 worldDirection = Camera.main.ScreenToWorldPoint(screenDirection) - Camera.main.transform.position;

                movement = worldDirection.normalized;
                timeLeft = accelerationTime;
            }

            transform.Translate(movement * maxSpeed * Time.deltaTime);
        }

        KeepWithinCameraBounds();
        
    }

    void ChangeDirection()
    {
        float randomX = UnityEngine.Random.Range(-5f, 5f);
        float randomY = UnityEngine.Random.Range(-5f, 5f);

        movement = new Vector2(randomX, randomY).normalized;
    }

    void KeepWithinCameraBounds()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPosition.x <= 0.05f || viewportPosition.x >= 0.95f)
        {
            movement.x = -movement.x;
            viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0.05f, 0.95f);
        }

        if (viewportPosition.y <= 0.05f || viewportPosition.y >= 0.95f)
        {
            movement.y = -movement.y;
            viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0.05f, 0.95f);
        }

        transform.position = mainCamera.ViewportToWorldPoint(viewportPosition);

    }
    void AimAtPlayer()
    {
        Vector3 direction = (Player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void ShootAtPlayer()
    {
        cooldownTimer -= Time.deltaTime;

        if(cooldownTimer <= 0f)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, transform.rotation);
            projectile.transform.Rotate(new Vector3(0, 0, -90));
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * projectileSpeed;
            rb.angularVelocity = 0;
            cooldownTimer = shootingcooldown;
        } 
    }
}
