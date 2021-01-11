using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float health = 1f;
    public float pointValue = 1;
    public bool diesOnContact = true;

    private EnemyManager manager;

    void Start() {
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet")
        {
            Damage(collision.gameObject.GetComponent<BulletTrigger>().GetDamage());
        }
        if (collision.gameObject.tag == "Stun Zone")
        {
            this.gameObject.SendMessage("Stun", collision.gameObject.GetComponent<StunZone>().lifetime);
        }
        if (collision.gameObject.tag == "Player")
        {
            if (diesOnContact)
            {
                Damage(1);
            }
        }
        
    }
    public void Die() {
        GameObject scoreDisplay = GameObject.FindWithTag("Score");
        scoreDisplay.SendMessage("addScore", pointValue);
        //manager.decrementEnemyCount();
        Destroy(this.gameObject);

    }
    void Damage(float amount) {
        health -= amount;
        if (health <= 0) {
            Die();
        }
    }
}
