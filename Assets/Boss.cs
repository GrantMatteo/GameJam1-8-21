using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float health = 100f;
    float maxHealth;
    public float pointValue = 100;
    public bool vulnerable = false;
    public GameObject healthBar;
    void Start()
    {
        maxHealth = health;
    }
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
        Destroy(this.gameObject);
        Time.timeScale = 0;
    }
    void Damage(float amount)
    {
        if (vulnerable)
        {
            health -= amount;
            healthBar.SendMessage("SetSize", health / maxHealth);
            if (health <= 0)
            {
                Die();
            }
        }
    }
}
