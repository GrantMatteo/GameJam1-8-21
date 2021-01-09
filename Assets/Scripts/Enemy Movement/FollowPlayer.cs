using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowPlayer : MonoBehaviour {
    private GameObject player;
    public float followStrength = 5f;
    private void Awake() {
        player = GameObject.Find("Player");
    }
    private void Update() {
        Vector2 towardPlayer = (player.transform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(towardPlayer * followStrength * Time.deltaTime);
    }
}
