using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Wall;
    public float speed;
    public int Health = 2;

    private float distance;
    public Rigidbody2D rb;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, Wall.transform.position);
        Vector2 direction = (Wall.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //transform.position = Vector2.MoveTowards(this.transform.position, Wall.transform.position, speed * Time.deltaTime);
        Vector2 direction34 = (Wall.transform.position - transform.position).normalized;
        if (rb != null)
        rb.AddForce(direction34 * speed * Time.deltaTime, ForceMode2D.Force);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            takedamange();
        }
    }

    void takedamange()
    {
        if ((Health - 1) <= 0)
        {
            die();
        }
        else
            Health--;
    }

    void die()
    {
        Destroy(gameObject);
    }
}
