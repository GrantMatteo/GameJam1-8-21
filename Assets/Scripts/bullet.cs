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
    //we'll probably need some Collision events with bullets to increase score and stuff
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    private void OnCollisionExit2D(Collision2D collision)
    {

    }
}
