using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Necessary Objects")]
    private Rigidbody2D rb;
    [Header("Controls")]
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    [Header("Player Params")]
    public float health = 3f;

    [Header("Movement Params")]
    public float acceleration = 150;
    public float maxSpeed = 10;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(up) && !(Input.GetKey(left) | Input.GetKey(right)))
        {
            rb.AddForce(transform.up * acceleration);
        }
        if (Input.GetKey(up) && Input.GetKey(right))
        {
            rb.AddForce(transform.up * acceleration * Mathf.Sqrt(0.5f));
            rb.AddForce(transform.right * acceleration * (Mathf.Sqrt(0.5f)));
        }
        if (Input.GetKey(up) && Input.GetKey(left)){
            rb.AddForce(transform.up * acceleration * Mathf.Sqrt(0.5f));
            rb.AddForce(-transform.right * acceleration * Mathf.Sqrt(0.5f));
        }
        if (Input.GetKey(down) && !(Input.GetKey(left)| Input.GetKey(right)))
        {
            rb.AddForce(-transform.up * acceleration);
        }
        if (Input.GetKey(down) && Input.GetKey(right))
        {
            rb.AddForce(-transform.up * acceleration * Mathf.Sqrt(0.5f));
            rb.AddForce(transform.right * acceleration * (Mathf.Sqrt(0.5f)));
        }
        if (Input.GetKey(down) && Input.GetKey(left))
        {
            rb.AddForce(-transform.up * acceleration * Mathf.Sqrt(0.5f));
            rb.AddForce(-transform.right * acceleration * Mathf.Sqrt(0.5f));
        }
        if (Input.GetKey(right) && !(Input.GetKey(up) | Input.GetKey(down)))
        {
            rb.AddForce(transform.right * acceleration);
        }
        if (Input.GetKey(left) && !(Input.GetKey(up) | Input.GetKey(down)))
        {
            rb.AddForce(-transform.right * acceleration);
        }
        print(acceleration);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Bump");
        if (collision.gameObject.tag == "Enemy")
        {
            Damage(1);
        }
    }

    public void Die()
    {
        //this.gameObject.SetActive(false);
    }

    void Damage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
           // Time.timeScale = 0;
        }
    }
}
