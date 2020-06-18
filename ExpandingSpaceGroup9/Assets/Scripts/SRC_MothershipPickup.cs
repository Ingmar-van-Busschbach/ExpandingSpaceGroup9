using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRC_MothershipPickup : MonoBehaviour
{
    private AudioSource audioData;
    private SRC_PlayerInventory playerInventory; //What script is going to be affected on player side
    private SRC_PlayerHealth playerHealth; //What script is going to be affected on player side
    private SRC_MothershipInventory mothershipInventory; //What script is going to be affected on own side

    void Start()
    {
        mothershipInventory = this.GetComponent<SRC_MothershipInventory>(); //Get own inventory
        audioData = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collision) //On volume enter event
    {
        if (collision.gameObject.tag == "Player") //If collided object is player
        {
            playerInventory = collision.gameObject.GetComponent<SRC_PlayerInventory>(); //Get script to affect
            playerInventory.TransferItems(mothershipInventory); //Transfer inventory
            playerHealth = collision.gameObject.GetComponent<SRC_PlayerHealth>(); //Get script to affect
            playerHealth.targetHealth = 100; //Set target health
            playerHealth.damagePerSecond = 1f; //Heal the player on entering
            audioData.Play(0); //Play sound effect
            playerHealth.OnHealed(0.5f, 500);

        }
    }
    void OnTriggerExit(Collider collision) //On volume enter event
    {
        if (collision.gameObject.tag == "Player") //If collided object is player
        {
            playerHealth = collision.gameObject.GetComponent<SRC_PlayerHealth>(); //Get script to affect
            playerHealth.targetHealth = 0; //Set target health
            playerHealth.damagePerSecond = 0.01f; //Set dakage per second to positive to damage the player again
            playerHealth.ResetVisualEffect();
        }
    }
}
