using UnityEngine;
using System.Collections;

public class FireHealth : MonoBehaviour {

    public int currentHealth = 5;
    public string currentRoom;
    public GameObject playerObject;

    public void Start()
    {
        playerObject = GameObject.Find("Character");
    }

    public void TakeDamage(int damage)
    {
        if (playerObject.GetComponent<PlayerHealth>().playerHealth > 0)
        {
            currentHealth -= damage;
        }
        if(currentHealth <= 0)
        {
            DestroyFire();
        }
    }

    public void DestroyFire()
    {
        switch(currentRoom){
            case "MedBay":
                GameObject.Find("GameController").GetComponent<ShipTracking>().fireInMedBay = false;
                break;
            case "Engine":
                GameObject.Find("GameController").GetComponent<ShipTracking>().fireInEngine = false;
                break;
            case "Cockpit":
                GameObject.Find("GameController").GetComponent<ShipTracking>().fireInCockpit = false;
                break;
            case "Empty":
                GameObject.Find("GameController").GetComponent<ShipTracking>().fireInEmpty = false;
                break;
        }
        playerObject.GetComponent<Animator>().SetBool("FireFighting", false);
        Destroy(this.gameObject);
    }
}
