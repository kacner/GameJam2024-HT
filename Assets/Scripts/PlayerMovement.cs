using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Main Movement Variables")]
    public float acceleration = 300f;
    public float maxSpeed = 3f;
    public bool CanMove = true;
    public float moveX;
    public float moveY;
    public Vector2 LastLookDir;
    public bool ShouldCameraFollow = false;

    [Header("Camera")]
    [SerializeField] private cameraScript CameraScript;

    [HideInInspector] public Vector2 moveDirection;
    [HideInInspector] private Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] private SpriteRenderer spriterenderer;
    
    [Space(10)]

    [Header("Particle Systems")]
    public ParticleSystem runningParticleSystem;

    [Space(10)]

    [Header("Mining")]
    [SerializeField] private int MiningDamage = 1;
    public float rayLength = 10f;
    private Vector2 mousePosition;
    private Vector2 startPosition;
    private Vector2 direction;
    public LayerMask targetLayerMask;
    public inventory Inventory;

    private GameObject[] allItems;
    public TextMeshProUGUI myText;

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
        
        if (moveDirection.y != 0)
        {
            LastLookDir = moveDirection;
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

        Mine();

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            allItems = GameObject.FindGameObjectsWithTag("Pickup");
            foreach (GameObject item in allItems)
            {
               Pickup pickupscript = item.GetComponent<Pickup>();
                if (pickupscript != null)
                    pickupscript.Dettach();
            }
        }
    }
    private void FixedUpdate()
    {
        myText.text = "Pearls: "+ Inventory.pearlsAmount.ToString();

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CameraSwitch")
        {
            CameraScript.Switch();
            StartCoroutine(ContinueWalk());
        }
        
        if (collision.tag == "ShouldFollow")
        {
            ShouldCameraFollow = true;
        }
        if (collision.tag == "ShouldNotFollow")
        {
            ShouldCameraFollow = false;
        }
    }

    IEnumerator ContinueWalk()
    {
        float duration = 1f;
        float time = 0;

        while (time < duration)
        {

            CanMove = false;
            Vector2 targetVelocity = LastLookDir * maxSpeed; // desired velocity based on input
            Vector2 velocityReq = targetVelocity - rb.velocity; // how much we need to change the velocity

            Vector2 moveforce = velocityReq * acceleration; //calculate the force needed to reach the target velocity considering acceleration

            rb.AddForce(moveforce * Time.fixedDeltaTime, ForceMode2D.Force); //applyes the movement to the rb

            acceleration = maxSpeed + 325 / 0.9f;


            time += Time.deltaTime;
            yield return null;
        }
        CanMove = true;
    }

    void Mine()
    {
        if (Input.GetMouseButton(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            startPosition = transform.position;

            direction = (mousePosition - startPosition).normalized;

            RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, rayLength, targetLayerMask);

            if (hit.collider != null && hit.collider.CompareTag("ForestTile"))
            {
                hit.collider.gameObject.GetComponent<ForestTile>().TakeDMG(MiningDamage);
            }


            if (hit.collider != null)
            {

                if (hit.collider.CompareTag("ForestTile"))
                {
                    hit.collider.gameObject.GetComponent<ForestTile>().TakeDMG(MiningDamage);
                }
            }
        }

        
    }
    void OnDrawGizmos()
    {
        if (direction != Vector2.zero)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(startPosition, startPosition + direction * rayLength);
        }
    }
}