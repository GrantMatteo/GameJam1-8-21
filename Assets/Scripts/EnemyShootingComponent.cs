using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingComponent : MonoBehaviour {

    public float enemyBulletSpeed = 10;
    public GameObject enemyBullet;
    private GameObject player;
    private bool activeShooter = false;
    private bool hasBullet = true;
    private GameObject enemyBulletInstance;

    void Start() {
        player = GameObject.Find("Player");
    }

    public void setActiveShooter(bool activeShooter) {
        this.activeShooter = activeShooter;
    }

    void Update()
    {
        if (activeShooter && hasBullet) {
            Vector2 towardPlayer = player.transform.position - transform.position;
            towardPlayer.Normalize();

            enemyBulletInstance = Instantiate(enemyBullet, transform.position, transform.rotation);
            enemyBulletInstance.GetComponent<Rigidbody2D>().velocity = towardPlayer * enemyBulletSpeed;
            hasBullet = false;
        }
    }


}
