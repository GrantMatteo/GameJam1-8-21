using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    private int wallCollisions = 0;

    public int wallCollisionsUntilExpiry = 2;
    //public int lifetimeUntilExpiry = 3000;

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
            enemyShootingComponent.addAmmo();
            wallCollisions = 0; // does this do anything?
            Destroy(this.gameObject);
        }
    }


}