using UnityEngine;
using System.Collections;

public class EmptyScript : MonoBehaviour {

    public bool inEmpty = false;
    public bool inPosition = false;
    public GameObject gameController;
    public GameObject playerObject;
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
        anim = playerObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        inEmpty = gameController.GetComponent<RoomTracking>().inEmpty;
        if (GameObject.Find("Character").GetComponent<PlayerMovement>().target == GameObject.Find("EmptyPosition"))
        {
            if (inPosition && !gameController.GetComponent<ShipTracking>().fireInEmpty)
            {
                anim.SetBool("Walking", false);
                anim.Play("Idle Animation");
            }
            else if (!inEmpty)
            {
                //turn off the lights
            }
        }
    }

    public void ActionToTake(int multiplier)
    {
        if (gameController.GetComponent<ShipTracking>().fireInEmpty)
        {
            ExtinguishFire(multiplier);
        }
        else
        {
            anim.SetBool("Walking", false);
            anim.SetBool("FireFighting", false);
            anim.SetBool("Idle",true);
        }
    }

    public void ExtinguishFire(int multiplier)
    {
        anim.SetBool("FireFighting", true);
        anim.SetBool("Walking", false);
        anim.SetBool("MedBayHealing", false);
        anim.SetBool("CockpitNav", false);
        anim.SetBool("Idle", false);
        anim.SetBool("EngineControl", false);
        GameObject.Find("EmptyFire").GetComponent<FireHealth>().TakeDamage(fireDamage * multiplier);
        playerObject.GetComponent<PlayerHealth>().TakeDamage(playerDamage);

    }
}
