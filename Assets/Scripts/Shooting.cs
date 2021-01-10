using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject bulletPowerBar;
    public GameObject bulletHalo;
    bool firing, positionLocked;
    float fireTimer;
    public float CHARGE_INIT_TIME= .05F;
    public float CHARGE_MAX_TIME = 3.05F;
    public float recoilAmount = 100;
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && hasBullet)
        {
            firing = true;
            fireTimer = Time.time;
            hasBullet = false;
        }
        if (firing && Time.time - fireTimer > CHARGE_INIT_TIME)
        {
            positionLocked = true;
            Debug.Log("Awfawef");
            float f = (Time.time - fireTimer) / CHARGE_MAX_TIME;
            bulletPowerBar.SendMessage("SetSize",f);
            this.gameObject.SendMessage("SetFiring", true);
        }
        if (firing && (Input.GetMouseButtonUp(0) || Time.time - fireTimer >= CHARGE_MAX_TIME))
        {

            Debug.Log("release " + positionLocked);
            this.gameObject.SendMessage("SetFiring", false);

            bulletPowerBar.SendMessage("SetSize", 0, 0);
            firing = false;
            if (positionLocked)
            {
                bigFireTowardsMouse(Mathf.Min((Time.time - fireTimer) / CHARGE_MAX_TIME, 1));
            }
            else
            {
                basicFireTowardsMouse();
            }
            positionLocked = false;
        }
        if (Input.GetMouseButtonDown(1) && !hasBullet)
        {
            teleportToBullet();
            hasBullet = true;
        }
        if (!firing)
        {
            bulletPowerBar.SendMessage("SetSize", 0, 0);
        }
    }

    void basicFireTowardsMouse()
    {
        Debug.Log("efawef");
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
    void bigFireTowardsMouse(float intensity)
    {
        Debug.Log("intensity" + intensity);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 velDirection = mousePos - new Vector2(playerTransform.position.x, playerTransform.position.y);
        velDirection.Normalize();

        bulletInstance = Instantiate(bullet, playerTransform.position, playerTransform.rotation);
        GameObject bulletHaloInstance = Instantiate(bulletHalo, playerTransform.position, playerTransform.rotation);
        bulletHaloInstance.transform.SetParent(bulletInstance.transform);
        bulletHaloInstance.transform.localScale *= 1 + 5*intensity;
        bulletHaloInstance.transform.position += new Vector3(0, 0, 1);//behind
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
        GetComponent<Rigidbody2D>().AddForce(intensity * (-velDirection)*recoilAmount);
    }
    void ChangeNextBullet(PowerupType type)
    {
        bulletPowerup = type;
    }
    void teleportToBullet()
    {
        Debug.Log("Bullet cam" + bulletCam);
        bulletCam.SendMessage("Deactiv");
        playerTransform.position = bulletInstance.GetComponent<Transform>().position;
        playerRB.velocity = bulletInstance.GetComponent<Rigidbody2D>().velocity;
        Destroy(bulletInstance);
    }
}