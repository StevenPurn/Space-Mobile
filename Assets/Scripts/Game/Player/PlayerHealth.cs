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
        playerHealth += changeValue;

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
    }
}