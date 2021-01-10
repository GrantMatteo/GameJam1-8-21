using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearScreen : MonoBehaviour
{
    public float lifetime = 2;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > lifetime)
        {
            Destroy(this.gameObject);
        }
    }
}

