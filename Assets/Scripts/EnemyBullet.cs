using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    private int wallCollisions = 0;
    private int lifetimeLeft;

    public int wallCollisionsUntilExpiry;
    public int lifetimeUntilExpiry;

    private EnemyShootingComponent enemyShootingComponent;

    // mother of anti-patterns: this HAS to be called when the object is made
    public void setEnemyShootingComponent(EnemyShootingComponent enemyShootingComponent) {
        this.enemyShootingComponent = enemyShootingComponent;
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Wall") {
            wallCollisions++;
        }
        if (wallCollisions == wallCollisionsUntilExpiry) {
            die();
        }
    }

    private void die() {
        enemyShootingComponent.addAmmo();
        wallCollisions = 0; // does this do anything?
        Destroy(this.gameObject);
    }

    void Start() {
        lifetimeLeft = lifetimeUntilExpiry;
    }

    void Update() {
        lifetimeLeft--;
        if (lifetimeLeft == 0) {
            die();
        }
    }


}