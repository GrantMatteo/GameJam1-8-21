using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHalo : MonoBehaviour
{
    public float lifeTime, bigTime;
    float startTime;
    Vector3 origScale;
    void Start()
    {
        startTime = Time.time;
    }
    void SetDamage(float x)
    {
        lifeTime = x;
        bigTime = x / 5;
        origScale = this.gameObject.transform.localScale / (1 + x);
    }
    // Update is called once per frame
    void Update()
    {
        
        this.gameObject.transform.localScale = origScale * (1 + lifeTime * (1 - (Time.time - startTime)/lifeTime));
        if (Time.time - startTime > lifeTime)
        {
            this.gameObject.transform.parent.gameObject.GetComponent<CircleCollider2D>().enabled = true;
            Destroy(this.gameObject);
        }
    }
}
