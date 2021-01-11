using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ChargePlayer : MonoBehaviour
{
    private GameObject player;
    public float followStrength = 60f;
    public float maxSpeed = 1.0000000001f;
    public float maxMaxSpeed = 150f;
    public float chargeStength = 10000;
    public float chargeDistance= 40;
    public float chargeWaitTime = 2;
    public float chargeDuration = 3;
    public float chargeCooldown = 3;
    private Rigidbody2D rb;
    private EnemyShootingComponent shootingComponent;

    private void Awake()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        shootingComponent = this.gameObject.GetComponent<EnemyShootingComponent>();
    }
    bool charging = false;
    Vector2 chargeForce;
    float chargeTimer=0;

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
    private void Update()
    {
        if (stunned)
        {
            if (Time.time - stunTimer > stunDuration)
            {
                stunned = false;
            }
            else
            {
                rb.velocity = new Vector3(0, 0, 0);
                return;
            }
        }
        Vector2 towardPlayer = player.transform.position - transform.position;
        Vector2 steering = towardPlayer.normalized;
        if (!charging && (towardPlayer.sqrMagnitude > chargeDistance || (Time.time - chargeTimer - chargeWaitTime > chargeDuration && Time.time - chargeTimer - chargeWaitTime - chargeDuration < chargeCooldown)))
        {
            Vector2 force = steering * followStrength * Time.deltaTime;
            float dist = towardPlayer.magnitude;

            Debug.Log("normal");
            rb.AddForce(force);
            followStrength = followStrength * maxSpeed;
        } else
        {
            if (!charging)
            {

                Debug.Log("startCharge");
                charging = true;
                chargeTimer = Time.time;
                chargeForce = steering * chargeStength * Time.deltaTime;
            }
            else if (Time.time - chargeTimer < chargeWaitTime) {

                Debug.Log("pauseCharge");
                rb.velocity = new Vector3(0, 0, 0);
            } else if (Time.time - chargeTimer - chargeWaitTime < chargeDuration)
            {

                Debug.Log("charge");
                rb.AddForce(chargeForce);
            } else
            {

                Debug.Log("endCharge");
                charging = false;
            }
        }

    }
}
