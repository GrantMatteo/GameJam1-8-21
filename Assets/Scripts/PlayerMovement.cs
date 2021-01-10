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
    public Animator animator;

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




    // Start is called before the first frame update
    void Start()
    {
        heldPowerup[0] = PowerupType.NONE;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        if (Input.GetKey(up) && !(Input.GetKey(left) | Input.GetKey(right)))
        {
            rb.AddForce(transform.up * acceleration * Time.deltaTime);
        }
        if (Input.GetKey(up) && Input.GetKey(right))
        {
            rb.AddForce(transform.up * acceleration * Mathf.Sqrt(0.5f) * Time.deltaTime);
            rb.AddForce(transform.right * acceleration * (Mathf.Sqrt(0.5f)) * Time.deltaTime);
        }
        if (Input.GetKey(up) && Input.GetKey(left)){
            rb.AddForce(transform.up * acceleration * Mathf.Sqrt(0.5f) * Time.deltaTime);
            rb.AddForce(-transform.right * acceleration * Mathf.Sqrt(0.5f) * Time.deltaTime);
        }
        if (Input.GetKey(down) && !(Input.GetKey(left)| Input.GetKey(right)))
        {
            rb.AddForce(-transform.up * acceleration * Time.deltaTime);
        }
        if (Input.GetKey(down) && Input.GetKey(right))
        {
            rb.AddForce(-transform.up * acceleration * Mathf.Sqrt(0.5f) * Time.deltaTime);
            rb.AddForce(transform.right * acceleration * (Mathf.Sqrt(0.5f)) * Time.deltaTime);
        }
        if (Input.GetKey(down) && Input.GetKey(left))
        {
            rb.AddForce(-transform.up * acceleration * Mathf.Sqrt(0.5f) * Time.deltaTime);
            rb.AddForce(-transform.right * acceleration * Mathf.Sqrt(0.5f) * Time.deltaTime);
        }
        if (Input.GetKey(right) && !(Input.GetKey(up) | Input.GetKey(down)))
        {
            rb.AddForce(transform.right * acceleration * Time.deltaTime);
        }
        if (Input.GetKey(left) && !(Input.GetKey(up) | Input.GetKey(down)))
        {
            rb.AddForce(-transform.right * acceleration * Time.deltaTime);
        }
        if (Input.GetKey(powerupUse) && heldPowerup[0] != PowerupType.NONE)
        {
            usePowerup();
        }
        healthbar.text = "Health: " + health.ToString();

       
    }
    public GameObject powerupDisplay;
    //changing this to public to see if it changes anything with interactionobject
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Damage(1);
        } else if (collision.gameObject.tag == "Powerup")
        {
            if (heldPowerup[0] == PowerupType.NONE)
            {
                collision.gameObject.SendMessage("Pickup", heldPowerup);
                powerupDisplay.SendMessage("SetImage", heldPowerup[0]);
            }
        }
        if(collision.CompareTag("InteractionObject"))
        {
            print(collision.name);
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
        if (collision.CompareTag("InteractionObject"))
        {
            print(collision.name + "leaving ");
            currentInterObj = null;
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
        powerupDisplay.SendMessage("SetImage", PowerupType.NONE);
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

