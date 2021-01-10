using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowPlayer : MonoBehaviour {
    private GameObject player;
    public float followStrength = 60f;
    public float maxSpeed = 1.0000000001f;
    public float maxMaxSpeed = 150f;
    private Rigidbody2D rb;
    private EnemyShootingComponent shootingComponent;

    private void Awake() {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        shootingComponent = this.gameObject.GetComponent<EnemyShootingComponent>();
    }
    private void Update() {
        Vector2 towardPlayer = player.transform.position - transform.position;
        Vector2 steering = towardPlayer.normalized;
        Vector2 force = steering * followStrength * Time.deltaTime;
        float dist = towardPlayer.magnitude;

        if (shootingComponent != null) {
            if (dist < shootingComponent.distanceFromPlayer) {
                force *= -1; // puts distance
            } else if (dist < shootingComponent.distanceForShooting) {
                force = Vector2.zero; // maintains distance
                shootingComponent.SendMessage(nameof(EnemyShootingComponent.setActiveShooter), true);
            } else {
                shootingComponent.SendMessage(nameof(EnemyShootingComponent.setActiveShooter), false);
            }
            
        }
        rb.AddForce(force);

        followStrength = followStrength * maxSpeed;

    }
}
