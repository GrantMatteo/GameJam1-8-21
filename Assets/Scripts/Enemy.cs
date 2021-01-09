using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float health = 10f;

    //we'll probably need some Collision events
    //with bullets: take damage
    //with player: hurt player
    private void OnCollisionEnter2D(Collision2D collision) {

    }

    void Damage(float amount) {
        health -= amount;
        if (health <= 0) {
            Die();
        }
    }
    void Die() { }
}
