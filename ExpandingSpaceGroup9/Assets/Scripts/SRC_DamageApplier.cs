using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRC_DamageApplier : MonoBehaviour
{
    private SRC_PlayerHealth playerHealth; //What script is going to be affected on player side
    [SerializeField] private float damage = 10; //Ammount of damage to deal

    void OnCollisionEnter(Collision collision) //On collision event
    {
        if (collision.gameObject.tag == "Player") //If collided object is player
        {
            playerHealth = collision.gameObject.GetComponent<SRC_PlayerHealth>(); //Get script to affect
            playerHealth.DealDamage(damage); //Deal damage
            //Destroy(this.gameObject); //Destroy self
        }
    }
}
