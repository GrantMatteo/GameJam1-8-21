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
    public bool telebackUnlocked = false, stunUnlocked = false, chargeShotUnlocked = false;

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
    public float TELEBACK_TIME = 1F;
    public float recoilAmount = 100;
    public GameObject bulletPowerContainer;
    // Update is called once per frame
    void Update()
    {
        if (telebackCapable && Time.time - telebackTimer > TELEBACK_TIME)
        {
            telebackCapable = false;
            Destroy(telebackPlatformInstance);
        }
        if (teleTimered && Time.time - teleTimer > teleTimerLength)
        {
            teleTimered = false;
        }
        if (chargeShotUnlocked)
        {
            if (!teleTimered && Input.GetMouseButtonDown(0) && hasBullet)
            {
                firing = true;
                fireTimer = Time.time;
                hasBullet = false;
            }
            if (firing && Time.time - fireTimer > CHARGE_INIT_TIME)
            {
                positionLocked = true;
                float f = (Time.time - fireTimer) / CHARGE_MAX_TIME;
                bulletPowerBar.SendMessage("SetSize", f);
                this.gameObject.SendMessage("SetFiring", true);
            }
            if (!teleTimered && firing && (Input.GetMouseButtonUp(0) || Time.time - fireTimer >= CHARGE_MAX_TIME))
            {
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
            bulletPowerContainer.SetActive(true);
        }
        else 
        {
            bulletPowerContainer.SetActive(false);
            if (!teleTimered && Input.GetMouseButtonDown(0) && hasBullet)
            {
                basicFireTowardsMouse();
                hasBullet = false;
            }

        }
    
        if (!telebackCapable && Input.GetMouseButtonDown(1) && !hasBullet)
        {
            teleportToBullet();
            hasBullet = true;
        }
        else if (telebackUnlocked && telebackCapable && Input.GetMouseButtonDown(1))
        {
            teleBack();
        }
        if (!firing && chargeShotUnlocked)
        {
            bulletPowerBar.SendMessage("SetSize", 0, 0);
        }
        if (telebackUnlocked && telebackProjectileInstance && (telebackProjectileInstance.transform.position - telebackPos).sqrMagnitude < .1)
        {
            Destroy(telebackProjectileInstance);
        }
    }

    void basicFireTowardsMouse()
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
    void bigFireTowardsMouse(float intensity)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 velDirection = mousePos - new Vector2(playerTransform.position.x, playerTransform.position.y);
        velDirection.Normalize();

        bulletInstance = Instantiate(bullet, playerTransform.position, playerTransform.rotation);
        GameObject bulletHaloInstance = Instantiate(bulletHalo, playerTransform.position, playerTransform.rotation);
        bulletHaloInstance.transform.SetParent(bulletInstance.transform);
        bulletHaloInstance.transform.localScale *= 1 + 5*intensity;
        bulletInstance.transform.position += new Vector3(0, 0, 1);//behind
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
        bulletInstance.GetComponent<CircleCollider2D>().enabled = false;
        bulletInstance.GetComponent<Rigidbody2D>().velocity = velDirection * bulletSpeed;
        bulletInstance.BroadcastMessage("SetDamage", intensity * 20);
        GetComponent<Rigidbody2D>().AddForce(intensity * (-velDirection)*recoilAmount);
    }
    void ChangeNextBullet(PowerupType type)
    {
        bulletPowerup = type;
    }
    public GameObject telebackProjectile;
    public GameObject telebackPlatform;
    GameObject telebackPlatformInstance;
    GameObject telebackProjectileInstance;
    public float telebackProjSpeed;
    bool telebackCapable = false;
    float telebackTimer;
    Vector3 telebackPos;
    void teleBack()
    {
        if (telebackProjectileInstance)
        {
            Destroy(telebackProjectileInstance);
        }
        telebackProjectileInstance = Instantiate(telebackProjectile, playerTransform.position, playerTransform.rotation);
        Vector3 retPos = this.gameObject.transform.position;
        Vector3 direction = telebackPos - retPos;
        float angleFromHor = Vector3.SignedAngle(new Vector3(1, 0, 0), direction, new Vector3(0,0,1));
        telebackProjectileInstance.transform.rotation = Quaternion.Euler(0, 0, angleFromHor - 90);
        direction.Normalize();
        this.gameObject.transform.position = telebackPos;
        telebackProjectileInstance.GetComponent<Rigidbody2D>().velocity = direction * telebackProjSpeed;
        telebackCapable = false;
        Destroy(telebackPlatformInstance);
    }
    public GameObject stunZonePrefab;
    public float teleTimerLength = 1;
    float teleTimer;
    public bool teleTimered=false;
    void teleportToBullet()
    {
        teleTimered = true; 
        teleTimer = Time.time;

        if (telebackUnlocked)
        {
            if (telebackPlatformInstance)
            {
                Destroy(telebackPlatformInstance);
            }
            telebackPlatformInstance = Instantiate(telebackPlatform, playerTransform.position, playerTransform.rotation);
            telebackTimer = Time.time;
            telebackPos = this.gameObject.transform.position;
            telebackCapable = true;
        }
        bulletCam.SendMessage("Deactiv");
        playerTransform.position = bulletInstance.GetComponent<Transform>().position - new Vector3(0,0,1);
        playerRB.velocity = bulletInstance.GetComponent<Rigidbody2D>().velocity;

        if (stunUnlocked)
        {
            GameObject stunZoneInstance = Instantiate(stunZonePrefab, playerTransform.position, playerTransform.rotation);
            stunZoneInstance.transform.position += new Vector3(0, 0, 10);
        }
        Destroy(bulletInstance);
    }
}