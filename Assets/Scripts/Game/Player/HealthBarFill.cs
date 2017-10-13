using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarFill : MonoBehaviour {

    private static Image healthBar;

	// Use this for initialization
	void Start () {
        healthBar = GetComponent<Image>();
	}

    public static void ChangeFill()
    {
        //change the fill of the health bar
        float currFill = (float)PlayerHealth.playerHealth / (float)PlayerHealth.maxHealth;
        healthBar.fillAmount = currFill;

        //color the health bar based on the percentage health remaining
        if (currFill >= .4f)
        {
            healthBar.color = Color.green;
        }
        else if (currFill < .4f && currFill > .2f)
        {
            healthBar.color = Color.yellow;
        }
        else
        {
            healthBar.color = Color.red;
        }
    }
}
