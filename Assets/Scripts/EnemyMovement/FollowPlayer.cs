using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowPlayer : MonoBehaviour {
    private GameObject player;
    public float followStrength = 60f;
    public float maxSpeed = 100f;

    private Rigidbody2D rb;
    private void Awake() {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        Vector2 towardPlayer = player.transform.position - transform.position;
        Vector2 steering = (towardPlayer - rb.velocity).normalized;
        rb.AddForce(steering * followStrength * Time.deltaTime);
        if (rb.velocity.magnitude > maxSpeed) {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
