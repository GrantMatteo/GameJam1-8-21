using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    public bool inventory; //can be stored in inventory?
    public bool openable; //can be opened?
    public bool locked; //is locked?
    public GameObject itemNeeded; //item needed in order to interact with this item

    public void DoInteraction()
    {
        gameObject.SetActive(false);
    }
    public void Open()
    {
        print("open seasme");
        // Animator.SetBool("open", true);
    }
}
