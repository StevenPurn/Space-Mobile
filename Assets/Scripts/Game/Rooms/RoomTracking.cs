using UnityEngine;
using System.Collections;

public class RoomTracking : MonoBehaviour {

    public bool inMedBay, inEngine, inCockpit, inEmpty = false;
    public Animator anim;

    public void Start()
    {
        anim = GameObject.Find("Character").GetComponent<Animator>();
    }

    public void updateRoom(string currentRoom)
    {
        switch (currentRoom)
        {
            case "MedBay":
                inMedBay = true;
                inEngine = false;
                inCockpit = false;
                inEmpty = false;
                anim.SetBool("Idle", false);
                anim.SetBool("CockpitNav", false);
                anim.SetBool("EngineControl", false);
                //call medbay function
                break;
            case "Engine":
                inMedBay = false;
                inEngine = true;
                inCockpit = false;
                inEmpty = false;
                anim.SetBool("MedBayHealing", false);
                anim.SetBool("CockpitNav", false);
                anim.SetBool("Idle", false);
                //call engine function
                break;
            case "Cockpit":
                inMedBay = false;
                inEngine = false;
                inCockpit = true;
                inEmpty = false;
                anim.SetBool("MedBayHealing", false);
                anim.SetBool("Idle", false);
                anim.SetBool("EngineControl", false);
                //call cockpit function
                break;
            case "Empty":
                inMedBay = false;
                inEngine = false;
                inCockpit = false;
                inEmpty = true;
                anim.SetBool("MedBayHealing", false);
                anim.SetBool("CockpitNav", false);
                anim.SetBool("EngineControl", false);
                //call empty function
                break;
        }
    }
}
