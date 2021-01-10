using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingComponent : MonoBehaviour {

    public float enemyBulletSpeed;
    public GameObject enemyBullet;

    public int distanceForShooting = 6;
    public int distanceFromPlayer = 3;
    public int maxAmmo;
    public int maxCooldown; // update cycles between shots
    private GameObject player;
    private bool activeShooter = false;
    private int ammo;
    private int cooldown;
    private GameObject enemyBulletInstance;

    void Start() {
        player = GameObject.Find("Player");
        ammo = maxAmmo;
        cooldown = maxCooldown;
    }

    public void setActiveShooter(bool activeShooter) {
        this.activeShooter = activeShooter;
    }

    public void addAmmo() {
        this.ammo += 1;
    }

    void Update()
    {
        if (activeShooter && ammo > 0 && cooldown == 0) {
            Vector2 towardPlayer = player.transform.position - transform.position;
            towardPlayer.Normalize();

            // fire this bad boy
            enemyBulletInstance = Instantiate(enemyBullet, transform.position, transform.rotation);
            enemyBulletInstance.GetComponent<EnemyBullet>().setEnemyShootingComponent(this);
            enemyBulletInstance.GetComponent<Rigidbody2D>().velocity = towardPlayer * enemyBulletSpeed;
            ammo--;
            cooldown = maxCooldown;
        } else if (cooldown > 0) {
            cooldown--; 
        }
    }


}
