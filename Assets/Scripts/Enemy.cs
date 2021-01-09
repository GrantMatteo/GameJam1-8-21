using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float health = 10f;
    public float pointValue = 1;
    public GameObject scoreDisplay;

    private void OnTriggerEnter2D(Collider2D collision) {


        if (collision.gameObject.tag == "Bullet")
        {
            Damage(1);
            scoreDisplay.SendMessage("addScore", pointValue);
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {

    }
    public void Die()
    {
        Destroy(this.gameObject);
    }

    void Damage(float amount) {
        health -= amount;
        if (health <= 0) {
            Die();
        }
    }
}
