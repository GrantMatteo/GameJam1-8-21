﻿using System.Collections;
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
    bool stunned;
    float stunTimer;
    public float stunDuration;
    void Stun(float duration)
    {
        if (!stunned)
        {
            stunDuration = duration;
            stunTimer = Time.time;
            stunned = true;
        }
    }
    public bool faceMove = false;
    public float faceOffset = 180;
    public float minDist = 30F;
    public float offScreenBoost = 5;
    private void Update() {
        if (stunned)
        {
            if (Time.time - stunTimer > stunDuration)
            {
                stunned = false;
            } else
            {
                rb.velocity = new Vector3(0, 0, 0);
                return;
            }
        }
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
        if (dist > minDist)
        {
            force *= dist / minDist * offScreenBoost;
        }
        rb.AddForce(force);
        if (faceMove)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, faceOffset + (Mathf.Sign(towardPlayer.y)) * Vector3.Angle(new Vector3(1, 0, 0), new Vector3(towardPlayer.x, towardPlayer.y, 0)));
        }
        followStrength = followStrength * maxSpeed;

    }
}
