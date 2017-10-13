using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    public static int playerHealth = 20;
    public static int maxHealth = 20;
    public static Image healthBar;

    private void Start()
    {
        healthBar = GetComponentInChildren<Image>();
    }

    public static void changeHealth(int changeValue)
    {
        Debug.Log("current health is:" + playerHealth);

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
        HealthBarFill.ChangeFill();
    }

    public static void playerDeath()
    {
        AnimationController.SetAnimation("Dead");
        //GameObject.Find("GameController").GetComponent<ShipTracking>().GameOver(false);
    }
}