using JetBrains.Annotations;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Main Movement Variables")]
    public float acceleration = 300f;
    public float maxSpeed = 3f;
    public bool CanMove = true;
    public float moveX;
    public float moveY;

    [Header("Camera")]
    [SerializeField] private cameraScript CameraScript;

    [HideInInspector] public Vector2 moveDirection;
    [HideInInspector] private Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] private SpriteRenderer spriterenderer;
    
    [Space(10)]

    [Header("Particle Systems")]
    public ParticleSystem runningParticleSystem;

    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (CanMove)
        {
            moveX = Input.GetAxisRaw("Horizontal"); //value -1 or 1. left or right
            moveY = Input.GetAxisRaw("Vertical"); //value -1 or 1. down and up

            moveDirection = new Vector2(moveX, moveY).normalized;
        }

        if (rb != null && rb.velocity.magnitude > 4.5f)
        {
            runningParticleSystem.enableEmission = true;
            var emmitino = runningParticleSystem.emission;
            emmitino.rateOverDistance = 5f;
        }
        else if (rb != null && rb.velocity.magnitude > 2.5f)
        {
            runningParticleSystem.enableEmission = true;
            var emmitino = runningParticleSystem.emission;
            emmitino.rateOverDistance = 1f;
        }
        else
            runningParticleSystem.enableEmission = false;

        if (Input.GetKey(KeyCode.N))
            Debug.Break();
    }
    private void FixedUpdate()
    {
        if (rb != null && rb.velocity.magnitude < 0.01f && !CanMove)
        {
            rb.velocity = Vector2.zero;
        }
        if (rb != null)
        {
            Vector2 targetVelocity = moveDirection * maxSpeed; // desired velocity based on input
            Vector2 velocityReq = targetVelocity - rb.velocity; // how much we need to change the velocity

            Vector2 moveforce = velocityReq * acceleration; //calculate the force needed to reach the target velocity considering acceleration

            rb.AddForce(moveforce * Time.fixedDeltaTime, ForceMode2D.Force); //applyes the movement to the rb

            acceleration = maxSpeed + 325 / 0.9f;
        }
    }
    private void UpdateHorVer()
    {
        if (moveDirection != Vector2.zero) //if player isnt moving
        {
                animator.SetFloat("Horizontal", moveX);
                animator.SetFloat("Vertical", moveY);
        }
        else if (moveDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", moveX);
            animator.SetFloat("Vertical", moveY);
        
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CameraSwitch")
        {
            print("Collided");
            CameraScript.Switch();
            StartCoroutine(DisableWalk());
        }
    }

    IEnumerator DisableWalk()
    {
        float duration = 1.5f;
        float time = 0;

        while (time < duration)
        {
            CanMove = false;
            time += Time.deltaTime;
            yield return null;
        }
        CanMove = true;
    }
}