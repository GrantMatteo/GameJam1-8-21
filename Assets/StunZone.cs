using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunZone : MonoBehaviour
{
    public float lifetime;
    float startTime;
    public AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        sound.Play();
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
