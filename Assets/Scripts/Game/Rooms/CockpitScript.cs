using UnityEngine;
using System.Collections;

public class CockpitScript : MonoBehaviour
{

    public bool inCockpit = false;
    public bool inPosition = false;
    public GameObject gameController;
    public GameObject playerObject;
    public GameObject cockpit;
    public Animator anim;
    public float fireTimer = 1.0f;
    public float fireTimerActual;
    public int fireDamage = 1;
    public int playerDamage = 1;

    // Use this for initialization
    void Start()
    {

        gameController = GameObject.Find("GameController");
        playerObject = GameObject.Find("Character");
        cockpit = GameObject.Find("Cockpit");
        fireTimerActual = fireTimer;

        anim = playerObject.GetComponent<Animator>();
    }

    public void ActionToTake(int multiplier)
    {
        if (gameController.GetComponent<ShipTracking>().fireInCockpit)
        {
            ExtinguishFire(multiplier);
        }
        else
        {
            anim.SetBool("FireFighting", false);
            ControlShipRotation(multiplier);
        }
    }

    public void ControlShipRotation(int multiplier)
    {
        if (inPosition)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("CockpitNav",true);
            gameController.GetComponent<ShipTracking>().ShipRotation(inCockpit, multiplier * 20);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check game controller script if player is in cockpit
        inCockpit = gameController.GetComponent<RoomTracking>().inCockpit;

        if (!inCockpit)
        {
            gameController.GetComponent<ShipTracking>().ShipRotation(inCockpit, 1);
        }

        if (GameObject.Find("Character").GetComponent<PlayerMovement>().target == GameObject.Find("CockpitPosition"))
        {
            if (inPosition && !gameController.GetComponent<ShipTracking>().fireInCockpit)
            {
                anim.SetBool("Walking", false);
                anim.Play("CockpitNav");
            }
            else if (!inCockpit)
            {
                //turn off the lights
            }
        }
    }

    public void ExtinguishFire(int multiplier)
    {
        anim.SetBool("FireFighting", true);
        anim.SetBool("Walking", false);
        anim.SetBool("MedBayHealing", false);
        anim.SetBool("CockpitNav", false);
        anim.SetBool("Idle", false);
        anim.SetBool("CockpitNav", false);
        GameObject.Find("CockpitFire").GetComponent<FireHealth>().TakeDamage(fireDamage * multiplier);
        playerObject.GetComponent<PlayerHealth>().TakeDamage(playerDamage);

    }
}