using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    public Transform center;
    Transform[] giblets;

    Animator anim;

    bool exploded;

    // Start is called before the first frame update
    void Start()
    {
        center = (center) ? center : transform;
        giblets = gameObject.FindComponentsInChildrenWithTag<Transform>("Giblet");
        anim = GetComponent<Animator>();

        Rigidbody2D rb;
        Collider2D c;
        foreach (var giblet in giblets)
        {
            rb = giblet.GetComponent<Rigidbody2D>();
            c = giblet.GetComponent<Collider2D>();

            rb.bodyType = RigidbodyType2D.Kinematic;
            c.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Explode();
        }
    }

    public void Explode()
    {
        if (anim) anim.enabled = false;
        if (exploded) return;
        exploded = true;

        Rigidbody2D rb;
        Collider2D c;
        foreach (var giblet in giblets)
        {
            giblet.gameObject.layer = LayerMask.NameToLayer("Giblet");
            rb = giblet.GetComponent<Rigidbody2D>();
            c = giblet.GetComponent<Collider2D>();

            rb.bodyType = RigidbodyType2D.Dynamic;
            c.enabled = true;

            rb.AddExplosionForce(Random.Range(10f, 20f), center.position, 5f);
            rb.AddTorque(Random.Range(-10f, 10f), ForceMode2D.Impulse);
        }
    }
}
