using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{


    private GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        child = this.gameObject.GetComponent<Transform>().GetChild(0).gameObject;
        child.SendMessage("SetParent", this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public AudioSource hitSound, ricochetSound;
    //we'll probably need some Collision events with bullets to increase score and stuff
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("awefawefawefawfeawefawef");
        if (collision.gameObject.tag == "Enemy")
        {
            hitSound.Play();
        } else if (collision.gameObject.tag == "Wall")
        {
            ricochetSound.Play();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            hitSound.Play();
        }
        else if (collision.gameObject.tag == "Wall")
        {
            ricochetSound.Play();
        }
    }
void OnCollisionExit2D(Collision2D collision)
    {

    }
}
