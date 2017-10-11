using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EngineScript : MonoBehaviour
{
    public int shipSpeed;
    public int maxSpeed = 20;
    public bool inEngine = false;
    public bool inPosition = false;
    public GameObject gameController;
    public GameObject playerObject;
    public float fireTimer = 1.0f;
    public float fireTimerActual;
    public int fireDamage = 1;
    public int playerDamage = 1;
    public Animator anim;
    public Image fuelLevel;
    public int currentLevel;
    public int maxLevel = 100;
    public float fuelTimerActual;
    public float fuelTimer = 2.0f;
    public GameObject engineUI;
    public GameObject lowFuelWarning;
    public GameObject noFuelWarning;
    public Sprite noFuelEngine;
    public GameObject fuelText;
    public GameObject malfunctionWarning;

    public float malfunctionTimerActual;
    public float malfunctionTimer = 5.0f;
    public bool malfunction1Active = false;
    public bool malfunction2Active = false;


    // Use this for initialization
    void Start()
    {
        gameController = GameObject.Find("GameController");
        playerObject = GameObject.Find("Character");
        fuelLevel = GameObject.Find("FuelLevelImage").GetComponent<Image>();
        engineUI = GameObject.Find("MainSpeedUI");
        lowFuelWarning = GameObject.Find("Low_fuel");
        noFuelWarning = GameObject.Find("NO_fuel");
        malfunctionWarning = GameObject.Find("MalfunctionWarning");
        fuelText = GameObject.Find("FuelText");
        lowFuelWarning.SetActive(false);
        noFuelWarning.SetActive(false);
        malfunctionWarning.SetActive(false);
        anim = playerObject.GetComponent<Animator>();
        fuelTimerActual = fuelTimer;
        currentLevel = maxLevel;
        malfunctionTimerActual = malfunctionTimer;
        shipSpeed = maxSpeed/2;
    }

    public void ActionToTake(int multiplier)
    {
        if (gameController.GetComponent<ShipTracking>().fireInEngine)
        {
            ExtinguishFire(multiplier);
        }
        else
        {
            anim.SetBool("FireFighting", false);
            ChangeShipSpeed(multiplier);
        }
    }

    public void ChangeShipSpeed(int multiplier)
    {
        if (inPosition)
        {
            //change ship speed/pop up UI? increase fuel consumption
            anim.SetBool("EngineControl",true);
            anim.SetBool("Walking", false);
        }
    }

    //Call FuelTracker
    void Update()
    {
        inEngine = gameController.GetComponent<RoomTracking>().inEngine;

        if(inPosition && !gameController.GetComponent<ShipTracking>().fireInEngine)
        {
            malfunction1Active = false;
            malfunction2Active = false;
            malfunctionWarning.SetActive(false);
            malfunctionTimerActual = malfunctionTimer;

            if (GameObject.Find("Character").GetComponent<PlayerMovement>().target == GameObject.Find("EnginePosition"))
            {
                anim.SetBool("Walking", false);
                anim.Play("EngineControl");
            }
        }

        FuelTracker();

        malfunctionTimerActual -= Time.deltaTime;

        if(malfunctionTimerActual <= 0)
        {
            MalfunctionTracker(Random.Range(0, 5));
            malfunctionTimerActual = malfunctionTimer;
            if (malfunction1Active)
            {
                MalfunctionActions();
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
        anim.SetBool("EngineControl", false);
        Debug.Log(GameObject.Find("EngineFire").GetComponent<FireHealth>());
        GameObject.Find("EngineFire").GetComponent<FireHealth>().TakeDamage(fireDamage * multiplier);
        playerObject.GetComponent<PlayerHealth>().TakeDamage(playerDamage);

    }

    //Track current fuel level
    public void FuelTracker()
    {
        //track fuel level
        fuelTimerActual -= Time.deltaTime;

        if (fuelTimerActual <= 0)
        {
            fuelTimerActual = fuelTimer;
            currentLevel -= 1;
        }


        fuelLevel.fillAmount = (float)currentLevel/(float)maxLevel;

        if(fuelLevel.fillAmount <= .2 && fuelLevel.fillAmount > 0)
        {
              malfunctionWarning.SetActive(false);
              lowFuelWarning.SetActive(true);
        }else if(currentLevel <= 0)
        {
            malfunctionWarning.SetActive(false);
            lowFuelWarning.SetActive(false);
            noFuelWarning.SetActive(true);
            GameObject.Find("engine_room").GetComponent<SpriteRenderer>().sprite = noFuelEngine;
            currentLevel = 0;
        }

        fuelText.GetComponent<Text>().text = currentLevel.ToString();
    }

    public void MalfunctionTracker(int seed)
    {
        if (!malfunction1Active && !malfunction2Active)
        {
            switch (seed)
            {
                case 1:
                    //constantly changing random #
                    //call malfunction UI
                    malfunction1Active = true;
                    malfunctionWarning.SetActive(true);
                    shipSpeed = Random.Range(1, 10);
                    //Debug.Log("Malfunction1 Active");
                    break;
                case 2:
                    //constant but random
                    malfunction2Active = true;
                    malfunctionWarning.SetActive(true);
                    shipSpeed = Random.Range(1, 10);
                    //Debug.Log("Malfunction2 Active");
                    break;
                default:
                    //nothing happens
                    break;
            }
        }
    }

    public void MalfunctionActions()
    {
        shipSpeed = Random.Range(1, 10);
        GameObject.Find("SpeedText").GetComponent<Text>().text = shipSpeed.ToString();
    }

    public void UpdateShipSpeed(int change)
    {
        shipSpeed += change;

        if(shipSpeed >= maxSpeed)
        {
            shipSpeed = maxSpeed;
        }else if(shipSpeed <= 0)
        {
            shipSpeed = 1;
        }
    }
}