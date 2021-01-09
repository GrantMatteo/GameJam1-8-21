using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerupType powerupType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Pickup(PowerupType[] pType)
    {
        pType[0] = powerupType;
        Destroy(this.gameObject);
    }
}
public enum PowerupType
{
    NONE, HEALTH, CLEAR_SCREEN, BEAR_TRAP, MASSIVE_BULLET
}