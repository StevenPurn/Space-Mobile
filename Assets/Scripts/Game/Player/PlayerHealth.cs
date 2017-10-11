using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class  PlayerHealth : MonoBehaviour {

    public int playerHealth = 20;
    public int maxHealth = 20;
    public Image healthBar;
    public Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }


    public void changeHealth(int changeValue)
    {
        //function to alter the health value of the player
        playerHealth += changeValue;

        //ensure player isn't dead or over max health
        if(playerHealth <= 0)
        {
            playerHealth = 0;
            playerDeath();
        }else if(playerHealth >= maxHealth)
        {
            playerHealth = maxHealth;
        }

        //change the fill of the health bar
        healthBar.fillAmount = (float)playerHealth / (float)maxHealth;

        //color the health bar based on the percentage health remaining
        if((float)playerHealth / (float)maxHealth >= .4f)
        {
            healthBar.color = Color.green;
        }else if((float)playerHealth / (float)maxHealth < .4f && (float)playerHealth / (float)maxHealth > .2f)
        {
            healthBar.color = Color.yellow;
        }
        else
        {
            healthBar.color = Color.red;
        }

    }

    public void TakeDamage(int damageReceived)
    {
        playerHealth -= damageReceived;

        if(playerHealth <= 0)
        {
            playerHealth = 0;
            playerDeath();
        }

        changeHealth(0);
    }

    public void playerDeath()
    {
        //call game over screen
        anim.Play("Death");
        GameObject.Find("GameController").GetComponent<ShipTracking>().GameOver(false);
    }
}