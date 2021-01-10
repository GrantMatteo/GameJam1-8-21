using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float bulletSpeed = 10;
    public GameObject bullet;
    public GameObject bulletCam;
    private Transform playerTransform;
    private Rigidbody2D playerRB;


    PowerupType bulletPowerup;
    bool hasBullet = true; //do we have the bullet on us?
    private GameObject bulletInstance;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && hasBullet)
        {
            fireTowardsMouse();
            hasBullet = false;
        }
        else if (Input.GetMouseButtonDown(1) && !hasBullet)
        {
            teleportToBullet();
            hasBullet = true;
        }
    }

    void fireTowardsMouse()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 velDirection = mousePos - new Vector2(playerTransform.position.x, playerTransform.position.y);
        velDirection.Normalize();

        bulletInstance = Instantiate(bullet, playerTransform.position, playerTransform.rotation);
        switch (bulletPowerup)
        {
            case PowerupType.MASSIVE_BULLET:
                bulletInstance.transform.localScale *= 5;
                bulletInstance.GetComponent<Collider2D>().enabled = false;
                bulletPowerup = PowerupType.NONE;
                break;
            default:
                bulletPowerup = PowerupType.NONE;
                break;
        }
        bulletCam.SetActive(true);
        bulletCam.SendMessage("SetBullet", bulletInstance);
        bulletInstance.GetComponent<Rigidbody2D>().velocity = velDirection * bulletSpeed;
    }
    void ChangeNextBullet(PowerupType type)
    {
        bulletPowerup = type;
    }
    void teleportToBullet()
    {
        Debug.Log("Bullet cam" + bulletCam);
        bulletCam.SendMessage("Deactiv");
        bulletCam.SetActive(false);
        playerTransform.position = bulletInstance.GetComponent<Transform>().position;
        playerRB.velocity = bulletInstance.GetComponent<Rigidbody2D>().velocity;
        Destroy(bulletInstance);
    }
}