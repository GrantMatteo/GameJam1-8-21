﻿using System.Collections;
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
    Animator animator;
    public GameObject bossHealthbar;

    [Header("Controls")]
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode powerupUse = KeyCode.Space;

    [Header("Player Params")]
    public float health = 6f;
    public Text healthbar;
    PowerupType[] heldPowerup = new PowerupType[1];
    PowerupType activePowerup;
    public GameObject currentInterObj = null;
    public InteractionObject currentInterObjScript = null;
    public Inventory inventory;



    [Header("Movement Params")]
    public float acceleration = 150;
    public float maxSpeed = 10;
    float animSpeedMultiplier = 10f;




    // Start is called before the first frame update
    void Start()
    {
        bossHealthbar.SendMessage("SetSize", 1);
        heldPowerup[0] = PowerupType.NONE;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    float deadTimer = 0;
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Dead Clock" +(Time.time - deadTimer));
        if (dead && Time.time - deadTimer > 5)
        {
            SceneManager.LoadScene("Menu");
        } else if (dead)
        {
            return;
        }

        float acceleration = this.acceleration * (firing ? .5F : 1F);  
        //animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        //animator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        animator.speed = (rb.velocity.magnitude / maxSpeed) * animSpeedMultiplier;
        if (Input.GetKey(up) && !(Input.GetKey(left) | Input.GetKey(right)))
        {
            rb.AddForce(Vector2.up * acceleration * Time.deltaTime);
        }
        if (Input.GetKey(up) && Input.GetKey(right))
        {
            rb.AddForce(Vector2.up * acceleration * Mathf.Sqrt(0.5f) * Time.deltaTime);
            rb.AddForce(Vector2.right * acceleration * (Mathf.Sqrt(0.5f)) * Time.deltaTime);
        }
        if (Input.GetKey(up) && Input.GetKey(left)){
            rb.AddForce(Vector2.up * acceleration * Mathf.Sqrt(0.5f) * Time.deltaTime);
            rb.AddForce(-Vector2.right * acceleration * Mathf.Sqrt(0.5f) * Time.deltaTime);
        }
        if (Input.GetKey(down) && !(Input.GetKey(left) | Input.GetKey(right)))
        {
            rb.AddForce(-Vector2.up * acceleration * Time.deltaTime);
        }
        if (Input.GetKey(down) && Input.GetKey(right))
        {
            rb.AddForce(-Vector2.up * acceleration * Mathf.Sqrt(0.5f) * Time.deltaTime);
            rb.AddForce(Vector2.right * acceleration * (Mathf.Sqrt(0.5f)) * Time.deltaTime);
        }
        if (Input.GetKey(down) && Input.GetKey(left))
        {
            rb.AddForce(-Vector2.up * acceleration * Mathf.Sqrt(0.5f) * Time.deltaTime);
            rb.AddForce(-Vector2.right * acceleration * Mathf.Sqrt(0.5f) * Time.deltaTime);
        }
        if (Input.GetKey(right) && !(Input.GetKey(up) | Input.GetKey(down)))
        {
            rb.AddForce(Vector2.right * acceleration * Time.deltaTime);
        }
        if (Input.GetKey(left) && !(Input.GetKey(up) | Input.GetKey(down)))
        {
            rb.AddForce(-Vector2.right * acceleration * Time.deltaTime);
        }
       
        if (Input.GetKey(powerupUse) && heldPowerup[0] != PowerupType.NONE)
        {
            //usePowerup();
        }
        if (rb.velocity.sqrMagnitude > 0)
        {
            tutText.pushToMin(1);
        }
        healthbar.text = "Health: " + health.ToString();
        if (invulnerable && Time.time - invulnTimer > invulnDuration)
        {
            invulnerable = false;
        } else if (!invulnerable && inEnemies > 0)
        {
            Damage(1);
        }
       
    }
    public TutorialText tutText;
    int inEnemies = 0;
    //changing this to public to see if it changes anything with interactionobject
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Damage(1);
            inEnemies++;
        } else if (collision.gameObject.tag == "Powerup")
        {
            collision.gameObject.SendMessage("Pickup", heldPowerup);
            switch (heldPowerup[0])
            {
                case PowerupType.TELEBACK:
                    tutText.pushToMin(4);
                    this.GetComponent<Shooting>().telebackUnlocked = true;
                    GameObject.Find("EnemyManager").GetComponent<EnemyManager>().currentGameState = 1;                
                    break;
                case PowerupType.CHARGE_SHOT:
                    tutText.pushToMin(6);
                    this.GetComponent<Shooting>().chargeShotUnlocked = true;
                    GameObject.Find("EnemyManager").GetComponent<EnemyManager>().currentGameState = 2;
                    break;
                case PowerupType.STUN:
                    tutText.pushToMin(8);
                    this.GetComponent<Shooting>().stunUnlocked = true;
                    GameObject.Find("EnemyManager").GetComponent<EnemyManager>().currentGameState = 2;
                    GameObject.Find("Boss").GetComponent<Boss>().vulnerable = true;
                    break;
            }
        }
        if(collision.CompareTag("InteractionObject"))
        {
            currentInterObj = collision.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<InteractionObject>(); 
        }

        //interaction with InteractionObjects;
        if (collision.gameObject.tag == "InteractionObject" && currentInterObj)
        {
            //check if you can put it in inventory
            if (currentInterObjScript.inventory)
            {
                inventory.AddItem(currentInterObj);
                print("item added to inventory");
            }
            if (currentInterObjScript.openable)
            {
                if (currentInterObjScript.locked)
                {
                    //check to see if we have the object needed
                    //search inventory, if found, unlock object
                    if (inventory.FindItem(currentInterObjScript.itemNeeded))
                    {
                        //we found item needed
                        currentInterObjScript.locked = false;
                        print(currentInterObj.name + " was unlocked");
                    }
                    else
                    {
                        print(currentInterObj.name + " was not unlocked");
                    }

                }
                else
                {
                    print(currentInterObj.name + " is unlocked");
                    currentInterObjScript.Open();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            inEnemies--;
        }
        if (collision.CompareTag("InteractionObject"))
        {
            currentInterObj = null;
        }
    }

   /* void usePowerup()
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
    }*/
    bool firing = false;
    void SetFiring(bool fi)
    {
        firing = fi;
    }
    void clearScreen()
    {
        Instantiate(screenClearer, this.transform.position, this.transform.rotation);

    }
    void throwBearTrap()
    {
        Instantiate(bearTrap, this.transform.position, this.transform.rotation);

    }
    bool dead = false;
    public AudioSource playerDeathSound, playerHurtSound;
    public void Die()
    {
        dead = true;
        deadTimer = Time.time;
        GameObject.Find("MusicManager").GetComponent<AudioSource>().Stop();
        playerDeathSound.Play();
    }
    float invulnTimer;
    public float invulnDuration = 1;
    bool invulnerable = false;
    void Damage(float amount)
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnTimer = Time.time;
            health -= amount;
            //animator.SetTrigger("Hurt");
            if (health <= 0)
            {
                Die();
            } else
            {
                playerHurtSound.Play();
            }
        }
    }
}

