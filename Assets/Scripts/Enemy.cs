using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float health = 1f;
    public float pointValue = 1;



    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet")
        {
            Damage(1);

            GameObject scoreDisplay = GameObject.FindWithTag("Score");
            scoreDisplay.SendMessage("addScore", pointValue);
        }
        if (collision.gameObject.tag == "Player")
        {
            Damage(1);
        }
    }
    public void Die() {
        Destroy(this.gameObject);
    }

    void Damage(float amount) {
        health -= amount;
        if (health <= 0) {
            Die();
        }
    }
}
