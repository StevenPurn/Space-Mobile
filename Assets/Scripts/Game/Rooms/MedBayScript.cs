using UnityEngine;
using System.Collections;

public class MedBayScript : MonoBehaviour {

    public float healthTimerActual = 1.0f;
    public float healthTimer = 1.0f;
    public int fireDamage = 1;
    public int playerDamage = 1;
    public int healthRegen;
    public bool inMedBay = false;
    public bool inPosition = false;
    public GameObject gameController;
    public GameObject playerObject;
    public GameObject medBay;
    public Animator anim;
    public float fireTimer = 1.0f;
    public float fireTimerActual;

    // Use this for initialization
    void Start () {

        gameController = GameObject.Find("GameController");
        playerObject = GameObject.Find("Character");
        medBay = GameObject.Find("MedBay");
        anim = playerObject.GetComponent<Animator>();

	}

    public void ActionToTake(int multiplier)
    {
        if(gameController.GetComponent<ShipTracking>().fireInMedBay)
        {
            ExtinguishFire(multiplier);
        }
        else
        {
            anim.SetBool("FireFighting", false);
            HealthRegen(multiplier);
        }
    }

    public void HealthRegen(int multiplier)
    {
        if (playerObject.GetComponent<PlayerHealth>().playerHealth > 0)
        {
            if (inPosition)
            {
                anim.SetBool("Walking", false);
                anim.SetBool("MedBayHealing", true);
                playerObject.GetComponent<PlayerHealth>().changeHealth(healthRegen * multiplier);
            }
        }
    }

    // Update is called once per frame
    void Update() {

        //Check if character is in the MedBay
        inMedBay = gameController.GetComponent<RoomTracking>().inMedBay;

        if (!inMedBay)
        {
            healthTimerActual = healthTimer;
        }

        inMedBay = gameController.GetComponent<RoomTracking>().inMedBay;
        if (GameObject.Find("Character").GetComponent<PlayerMovement>().target == GameObject.Find("MedBayPosition"))
        {
            if (inPosition && !gameController.GetComponent<ShipTracking>().fireInMedBay)
            {
                anim.SetBool("Walking", false);
                anim.Play("MedBayHealing");
            }
            else if (!inMedBay)
            {
            }
        }
    }

   public void ExtinguishFire(int multiplier)
    {

        anim.SetBool("Walking", false);
        anim.SetBool("MedBayHealing", false);
        anim.SetBool("CockpitNav", false);
        anim.SetBool("Idle", false);
        anim.SetBool("EngineControl", false);
        anim.SetBool("FireFighting", true);
        GameObject.Find("MedBayFire").GetComponent<FireHealth>().TakeDamage(fireDamage * multiplier);
        playerObject.GetComponent<PlayerHealth>().TakeDamage(playerDamage);

    }
}
