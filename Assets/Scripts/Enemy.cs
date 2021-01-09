using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float health = 10f;

    //we'll probably need some Collision events with bullets to increase score and stuff
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Bullet")
        {
            //TODO increase score
            Death();
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {

    }
    public void Death()
    {
        Destroy(this.gameObject);
    }
}
