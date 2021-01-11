using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public AudioSource powerup, macguffin;
    public GameObject musicController;
    public GameObject[] destroyedWalls;
    public GameObject boss;
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
        if (musicController)
        {
            musicController.SendMessage("Pause");
        }
        pType[0] = powerupType;
        boss.SetActive(true);
        foreach (GameObject o in destroyedWalls)
        {
            Destroy(o);
        }
        if (powerupType < PowerupType.MASSIVE_BULLET)
        {
            powerup.Play();
        } else
        {
            macguffin.Play();
        }
        Destroy(this.gameObject);
    }
}
public enum PowerupType
{
    NONE, HEALTH, CLEAR_SCREEN, BEAR_TRAP, MASSIVE_BULLET, TELEBACK, CHARGE_SHOT, STUN, GREEN_KEY, INVINC
}