using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    private Vector3 direction;

    [Header("WindFX")]
    public GameObject WindFx;
    private SpriteRenderer WindFxSpriterenderer;
    private float WindFxAlpha = 1f;
    private float velocity;
    
    [HideInInspector] public Vector3 latePlayerPos;

    [HideInInspector] public float MaxChargeTime;
    [HideInInspector] public float ChargedTime;
    public int damange = 1;


    void Start()
    {
        WindFxSpriterenderer = WindFx.GetComponent<SpriteRenderer>();

        //mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        //mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //direction = mousePos - transform.position;
        //Vector3 rotation = transform.position - mousePos;
        //float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        DestroyArrow(5f);
    }

    private void Update()
    {
        if (WindFxSpriterenderer != null)
        {
            velocity = rb.velocity.magnitude;
            WindFxAlpha = Mathf.Clamp((velocity * 0.013f) + 0.316f, 0, 1);
            WindFxSpriterenderer.color = new Color(1, 1, 1, WindFxAlpha);

            WindFx.transform.localScale = new Vector3(0.025f * velocity + 1, 0.025f * velocity + 1, 0.025f * velocity + 1);
        }
    }

    IEnumerator DestroyArrow(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(WindFxSpriterenderer);
        Destroy(rb);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHp enemyHP = collision.GetComponent<EnemyHp>();

        if (enemyHP != null)
        {
            Debug.Log("Hit detected on enemy!");

            print(ChargedTime + "      " + MaxChargeTime);

            enemyHP.TakeDmg(damange, latePlayerPos, 20f, this.gameObject);

            applyKnockback(collision.transform.position, 8f);

            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void applyKnockback(Vector3 attackerPos, float knockbackAmount)
    {
        Vector3 transformPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector2 KBdir = (transformPos - attackerPos).normalized;
        rb.AddForce(KBdir * knockbackAmount, ForceMode2D.Impulse);
    }
}
