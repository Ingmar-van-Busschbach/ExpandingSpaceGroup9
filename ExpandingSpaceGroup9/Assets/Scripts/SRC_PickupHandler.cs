using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRC_PickupHandler : MonoBehaviour
{
    private SRC_PlayerInventory playerInventory; //What script is going to be affected on player side
    [SerializeField] private int itemIndex; //Item index

    void OnTriggerEnter(Collider collision) //On volume enter event
    {
        if (collision.gameObject.tag == "Player") //If collided object is player
        {
            Debug.Log(itemIndex + " Picked Up");
            playerInventory = collision.gameObject.GetComponent<SRC_PlayerInventory>(); //Get script to affect
            playerInventory.PickUpItem(itemIndex); //Pick up item
            Destroy(this.gameObject); //Destroy self
        }
    }
}
