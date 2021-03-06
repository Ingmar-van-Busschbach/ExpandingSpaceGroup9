﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SRC_MothershipInventory : SRC_PlayerInventory //Inherits from player inventory
{
    public SRC_ChangeScore ScoreManager2;

    public override void PickUpItem(int index) //Override because mothership does diffrent things than player
    {
        pickups[index] = true; //Sets item to true based on index of pick-up-able item
        OnPickup(index);
        if (pickups[0] && pickups[1] && pickups[2] && pickups[3] && pickups[4] && pickups[5] && pickups[6] && pickups[7]) //Check all items
        {
            OnFoundAll();
        }
        //UI Mothership inventory link here
    }

    public override void OnPickup(int index) //When player transfers item to mothership, override
    {
        int score = 0;
        for (int i = 0; i < pickups.Length; i++) //For each item
        {
            if (pickups[i])
            {
                score++;
            }
        }
        ScoreManager2.UpdateScore(score);
    }

    public void OnFoundAll() //If all items are found (victory)
    {
        SceneManager.LoadScene("GameVictory");
    }
}
