using UnityEngine;
using System.Collections;

public class MedBay : Rooms {

    public int healthRegen = 1;
    public GameObject playerObject;
    private float regenTimer;
    private float regenTime = 0.75f;

    // Use this for initialization
    public override void Init () {
        playerObject = GameObject.Find("Character");
        regenTimer = regenTime;
	}

    public override void RoomAction(int multiplier)
    {
        HealthRegen(multiplier);
    }

    public void HealthRegen(int multiplier)
    {
        int curHealth = PlayerHealth.playerHealth;

        regenTimer -= Time.deltaTime;
        if (regenTimer <= 0)
        {
            regenTimer = regenTime;
            if (curHealth > 0 && curHealth != PlayerHealth.maxHealth)
            {
                if (playerInPosition)
                {
                    AnimationController.SetAnimation("MedBayHealing");
                    PlayerHealth.changeHealth(healthRegen * multiplier);
                }
            }
        }
        else
        {
            AnimationController.SetAnimation("Idle");
        }
    }
}
