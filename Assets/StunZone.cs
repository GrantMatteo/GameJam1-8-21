using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunZone : MonoBehaviour
{
    public float lifetime;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }
    int frames = 0;
    // Update is called once per frame
    void Update()
    {
        if (frames == 2)
        {
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        } else
        {
            frames++;
        }
        if (Time.time - startTime > lifetime)
        {
            Destroy(this.gameObject);
        }
    }
}
