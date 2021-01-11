using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float health = 100f;
    public float pointValue = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
            
        }
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(collision.gameObject);
        }

    }
    public void Die()
    {
        GameObject scoreDisplay = GameObject.FindWithTag("Score");
        scoreDisplay.SendMessage("addScore", pointValue);
        Destroy(this.gameObject);

    }
    void Damage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }
}
