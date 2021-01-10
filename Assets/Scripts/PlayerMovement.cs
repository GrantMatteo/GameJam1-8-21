using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Necessary Objects")]
    private Rigidbody2D rb;
    public GameObject bearTrap;
    public GameObject screenClearer;

    [Header("Controls")]
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode powerupUse = KeyCode.Space;
    [Header("Player Params")]
    public float health = 6f;
    public Text healthbar;



    [Header("Movement Params")]
    public float acceleration = 150;
    public float maxSpeed = 10;


    PowerupType[] heldPowerup = new PowerupType[1];

    PowerupType activePowerup;

    // Start is called before the first frame update
    void Start()
    {
        heldPowerup[0] = PowerupType.NONE;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(up) && !(Input.GetKey(left) | Input.GetKey(right)))
        {
            rb.AddForce(transform.up * acceleration);
        }
        if (Input.GetKey(up) && Input.GetKey(right))
        {
            rb.AddForce(transform.up * acceleration * Mathf.Sqrt(0.5f));
            rb.AddForce(transform.right * acceleration * (Mathf.Sqrt(0.5f)));
        }
        if (Input.GetKey(up) && Input.GetKey(left)){
            rb.AddForce(transform.up * acceleration * Mathf.Sqrt(0.5f));
            rb.AddForce(-transform.right * acceleration * Mathf.Sqrt(0.5f));
        }
        if (Input.GetKey(down) && !(Input.GetKey(left)| Input.GetKey(right)))
        {
            rb.AddForce(-transform.up * acceleration);
        }
        if (Input.GetKey(down) && Input.GetKey(right))
        {
            rb.AddForce(-transform.up * acceleration * Mathf.Sqrt(0.5f));
            rb.AddForce(transform.right * acceleration * (Mathf.Sqrt(0.5f)));
        }
        if (Input.GetKey(down) && Input.GetKey(left))
        {
            rb.AddForce(-transform.up * acceleration * Mathf.Sqrt(0.5f));
            rb.AddForce(-transform.right * acceleration * Mathf.Sqrt(0.5f));
        }
        if (Input.GetKey(right) && !(Input.GetKey(up) | Input.GetKey(down)))
        {
            rb.AddForce(transform.right * acceleration);
        }
        if (Input.GetKey(left) && !(Input.GetKey(up) | Input.GetKey(down)))
        {
            rb.AddForce(-transform.right * acceleration);
        }
        if (Input.GetKey(powerupUse) && heldPowerup[0] != PowerupType.NONE)
        {
            usePowerup();
        }
        healthbar.text = "Health: " + health.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bump");
        if (collision.gameObject.tag == "Enemy")
        {
            Damage(1);
        } else if (collision.gameObject.tag == "Powerup")
        {
            if (heldPowerup[0] == PowerupType.NONE)
            {
                collision.gameObject.SendMessage("Pickup", heldPowerup);
                Debug.Log(heldPowerup[0]);
            }
        }
    }
    void usePowerup()
    {
        switch (heldPowerup[0])
        {
            case PowerupType.HEALTH:
                health++;
                break;
            case PowerupType.MASSIVE_BULLET:
                activePowerup = heldPowerup[0];
                this.gameObject.SendMessage("ChangeNextBullet", activePowerup);
                break;
            case PowerupType.CLEAR_SCREEN:
                clearScreen();
                break;
            case PowerupType.BEAR_TRAP:
                throwBearTrap();
                break;
        }
        heldPowerup[0] = PowerupType.NONE;
    }
    void clearScreen()
    {
        Instantiate(screenClearer, this.transform.position, this.transform.rotation);

    }
    void throwBearTrap()
    {
        Instantiate(bearTrap, this.transform.position, this.transform.rotation);

    }
    public void Die()
    {
        this.gameObject.SetActive(false);
    }

    void Damage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            //Die();
            //SceneManager.LoadScene("Menu");
            //Time.timeScale = 0;
        }
    }
}
