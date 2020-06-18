using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SRC_PlayerHealth : MonoBehaviour
{
    public float health = 100; //Starting health
    public float damagePerSecond = 0.01f; //Health per second that you lose for driving around etc
    public float targetHealth = 0; //Health to aim for with damage per second
    public Image healthBarCore; //UI Image for the health bar
    public Gradient damagedGradient; //Gradient for the red overlay color when you get damaged
    public Gradient healedGradient; //Gradient for the green overlay color when you get healed

    public void DealDamage(float damage) //Damage function. Input negative float damage to heal
    {
        health = health - damage; //Subtract damage from health
        if (health <= 0) //If player dies
        {
            OnDeath(); //Execute on death
        }
        if (damage > 0)
        {
            OnDamaged(); //Execute on damaged
        }
        if (damage < 0)
        {
            OnHealed(); //Execute on damaged
        }
    }

    public void OnDeath() //Executed on player death
    {
        SceneManager.LoadScene("GameOver");
    }

    public void OnDamaged(float color = 1, float time = 1) //Executed on player damaged
    {
        RenderSettings.ambientLight = damagedGradient.Evaluate(color); //Set visual feedback to a red color when damaged
        Invoke("ResetVisualEffect", time); //Trigger the reset function after <time> seconds
    }

    public void OnHealed(float color = 1, float time = 1) //Executed on player damaged
    {
        RenderSettings.ambientLight = healedGradient.Evaluate(color); //Set visual feedback to a green color when healed
        Invoke("ResetVisualEffect", time); //Trigger the reset function after <time> seconds
    }

    public void ResetVisualEffect()
    {
        RenderSettings.ambientLight = healedGradient.Evaluate(0); //Reset visual feedback after being damaged or healed
    }

    void Update() // Update is called once per frame
    {
        health = Mathf.Lerp(health, targetHealth, damagePerSecond * Time.deltaTime); //Health must go down at a constant rate
        var healthBarTransform = healthBarCore.transform as RectTransform; //Make new transform variable for the healthbar UI image
        healthBarTransform.sizeDelta = new Vector2(health, healthBarTransform.sizeDelta.y); //Set the width of the healthbar UI equal to health * scale, so it goes down if health goes down
        //Debug.Log(health); //Debug log health
    }
}
