using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBox : MonoBehaviour
{
    public int inCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("enc obj");
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        inCount++;
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            GameObject child= this.gameObject.transform.GetChild(i).gameObject;
            child.gameObject.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        inCount--;
        if (inCount == 0)
        {
            for (int i = 0; i < this.gameObject.transform.childCount; i++)
            {
                GameObject child = this.gameObject.transform.GetChild(i).gameObject;
                child.gameObject.SetActive(false);
            }
        }

    }
}
