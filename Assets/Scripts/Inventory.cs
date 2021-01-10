using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] inventory = new GameObject[3];

    public void AddItem(GameObject item)
    {
        bool itemAdded = false;
        //find an empty slot
        for (int i = 0; i < inventory.Length; i++){
            if(inventory[i] == null)
            {
                inventory[i] = item;
                print(item.name + " was added.");
                itemAdded = true;
                //do something with the object
                item.SendMessage("DoInteraction");
                break;
            }
        }
        if (!itemAdded)
        {
            print("Inventory Full--Not Added");
        }
    }
   public bool FindItem(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++){
            if (inventory[i] == item)
            {
                return true;
            }
            }
        return false;
        }
}
