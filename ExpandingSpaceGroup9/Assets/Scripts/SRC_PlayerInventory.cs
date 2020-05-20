using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRC_PlayerInventory : MonoBehaviour
{
    public bool[] pickups = new bool[8]; //Array of items found

    public virtual void PickUpItem(int index) //Add item function
    {
        pickups[index] = true; //Sets item to true based on index of pick-up-able item
        OnPickup(index);
        //UI inventory link here
    }

    public void RemoveItem(int index) //Remove item function for dropping items off at mothership
    {
        pickups[index] = false; //Sets item to false based on index of pick-up-able item
        //UI inventory link here
    }

    public virtual void OnPickup(int index) //When player picks up item
    {

    }

    public void TransferItems(SRC_MothershipInventory targetInventory) //Target inventory to transfer to
    {
        for (int i = 0; i < pickups.Length; i++) //For each item
        {
            if(pickups[i])
            {
                targetInventory.PickUpItem(i); //Add item to target inventory
                RemoveItem(i); //Remove item from own inventory

            }
        }
    }
}
