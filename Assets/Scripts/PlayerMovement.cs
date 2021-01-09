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

    
    [Header("Movement Params")]
    public float acceleration = 1;
    public float maxSpeed = 10;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(up)!(Input.GetKey(left) | Input.GetKey(right)))
        {
            rb.AddForce(transform.up * acceleration);
            print("up");
        }
        if (Input.GetKey(down) && !(Input.GetKey(left)| Input.GetKey(right)))
        {
            rb.AddForce(-transform.up * acceleration);
            print("down");
        }
        if (Input.GetKey(right))
        {
            rb.AddForce(transform.right * acceleration);
        }
        if (Input.GetKey(left))
        {
            rb.AddForce(-transform.right * acceleration);
        }
        if(Input.GetKey(left) && Input.GetKey(up))
        {
            print("test");
        }
    }
}
