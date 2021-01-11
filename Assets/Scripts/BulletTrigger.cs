using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    //    this.transform.position = parent.transform.position;
    //    this.transform.rotation = parent.transform.rotation;
    }
    public float damage = 1;
    public float GetDamage()
    {
        return damage;
    }
    void SetDamage(float x)
    {
        damage = x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    private void SetParent(GameObject o)
    {
        parent = o;
    }
    
}
