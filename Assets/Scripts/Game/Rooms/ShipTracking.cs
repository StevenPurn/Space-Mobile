using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipTracking : MonoBehaviour {

    public GameObject shipObject;
    public GameObject medBayTarget;
    public GameObject cockpitTarget;
    public GameObject engineTarget;
    public GameObject emptyTarget;
    public GameObject playerObject;
    public GameObject fireParticles;
    public GameObject instantiatedFire;
    public GameObject instantiatedLanding;
    public GameObject successfulLanding;
    public float maxRotation = 15.0f;
    public float currentRotationRate = 0.0f;
    public float rotationRate = 2.0f;
    public float fireDelay = 0.0f;
    public float fireTimer = 5.0f;
    public float currentHeight;
    public float trackedHeight;
    public float heightMultiplier;
    public float maxHeight = 10000f;
    public bool fireInMedBay = false;
    public bool fireInCockpit = false;
    public bool fireInEngine = false;
    public bool fireInEmpty = false;
    public bool landedOrDead = false;
    public int clicksInRoom = 0;
    public float clickTimerActual = 0f;
    public float clickTimer = 1.0f;
    public GameObject gameOverCanvas;
    public bool shipMoving = true;

	// Use this for initialization
	void Start () {
        shipObject = GameObject.Find("Ship");
        fireDelay = fireTimer;
        engineTarget = GameObject.Find("EnginePosition");
        cockpitTarget = GameObject.Find("CockpitPosition");
        medBayTarget = GameObject.Find("MedBayPosition");
        emptyTarget = GameObject.Find("EmptyPosition");
        playerObject = GameObject.Find("Character");
        gameOverCanvas = GameObject.Find("GameOverPanel");
        gameOverCanvas.SetActive(false);
        ResetClickCounter();
        currentHeight = maxHeight;
        shipMoving = true;
    }
	
    public void GameOver(bool playerWon)
    {
        gameOverCanvas.SetActive(true);

        if (playerWon)
        {
            //call game over UI with success text
        }
        else
        {
            //call game over UI with lose text
        }
    }

	// Update is called once per frame
	void Update () {

        ShipSpeed();

        if (shipMoving)
        {
            ShipHeight();
        }

        ClickTimingTracker();

        fireDelay -= Time.deltaTime;

        if(fireDelay < 0)
        {
            fireDelay = fireTimer;
            FireStarter(Random.Range(0,4));
        }

        if (Input.GetMouseButtonDown(0))
        {
            ClickTracker();
        }

    }

    public void CrashLanding()
    {
        landedOrDead = true;
        //play crashing animation
    }

    public void SafeLanding()
    {
        SceneManager.LoadScene("SuccessfulLanding");
    }

    public void ShipRotation(bool rotationControlled, int multiplier)
    {
        if (!landedOrDead)
        {
            if (rotationControlled)
            {
                currentRotationRate -= 2 *  multiplier * Time.deltaTime;
            }
            else
            {
                currentRotationRate += rotationRate * Time.deltaTime;
            }

            currentRotationRate = Mathf.Clamp(currentRotationRate, -maxRotation, maxRotation);

            shipObject.transform.rotation = Quaternion.Euler(0, 0, currentRotationRate);
        }
    }

    public void ShipSpeed()
    {
        //track the current speed of the ship
        GameObject.Find("SpeedText").GetComponent<Text>().text = GameObject.Find("Engine").GetComponent<EngineScript>().shipSpeed.ToString() + " MPH";
    }

    public void ShipHeight()
    {
        if (!landedOrDead)
        {
            //track the altitude of the ship
            //current height -= speed * angle?
            //((speed -1) * angle)/15

            heightMultiplier = ((GameObject.Find("Engine").GetComponent<EngineScript>().shipSpeed - 1) * currentRotationRate)/ maxRotation;

            currentHeight -= (9.8f - (heightMultiplier)); //* Time.deltaTime * Time.deltaTime;

            GameObject.Find("AltitudeText").GetComponent<Text>().text = currentHeight.ToString() + " FT";

            Vector3 shipPosition = new Vector3(0, -10f * (1f - (currentHeight / maxHeight)));

            GameObject.Find("Ship").transform.position = shipPosition;
        }
    }

    public void FireStarter(int randomSeed)
    {
        if (!landedOrDead)
        {
            switch (randomSeed)
            {
                case 0:
                    if (!fireInMedBay)
                    {
                        fireInMedBay = true;
                        instantiatedFire = (GameObject)Instantiate(fireParticles, GameObject.Find("MedBayFireSpawn").transform.position, GameObject.Find("MedBayFireSpawn").transform.rotation);
                        instantiatedFire.transform.parent = medBayTarget.transform;
                        instantiatedFire.GetComponent<FireHealth>().currentRoom = "MedBay";
                        instantiatedFire.name = "MedBayFire";
                    }
                    break;
                case 1:
                    if (!fireInCockpit)
                    {
                        fireInCockpit = true;
                        instantiatedFire = (GameObject)Instantiate(fireParticles, GameObject.Find("CockpitFireSpawn").transform.position, GameObject.Find("CockpitFireSpawn").transform.rotation);
                        instantiatedFire.transform.parent = cockpitTarget.transform;
                        instantiatedFire.GetComponent<FireHealth>().currentRoom = "Cockpit";
                        instantiatedFire.name = "CockpitFire";
                    }
                    break;
                case 2:
                    if (!fireInEngine)
                    {
                        fireInEngine = true;
                        instantiatedFire = (GameObject)Instantiate(fireParticles, GameObject.Find("EngineFireSpawn").transform.position, GameObject.Find("EngineFireSpawn").transform.rotation);
                        instantiatedFire.transform.parent = engineTarget.transform;
                        instantiatedFire.GetComponent<FireHealth>().currentRoom = "Engine";
                        instantiatedFire.name = "EngineFire";
                    }
                    break;
                case 3:
                    if (!fireInEmpty)
                    {
                        fireInEmpty = true;
                        instantiatedFire = (GameObject)Instantiate(fireParticles, GameObject.Find("EmptyFireSpawn").transform.position, GameObject.Find("EmptyFireSpawn").transform.rotation);
                        instantiatedFire.transform.parent = emptyTarget.transform;
                        instantiatedFire.GetComponent<FireHealth>().currentRoom = "Empty";
                        instantiatedFire.name = "EmptyFire";
                    }
                    break;
                default:
                    Debug.Log("no fire");
                    break;
            }
        }
    }

    public void ClickTracker()
    {
        //raycast from mouse and check which room it collides with
        //also check inROOM varibales to determine if its the same room 
        //check time from last click to determine if the double function
        //should be called
        //maybe just count clicks between timer if all are in the same room
        //reset count if a new room is clicked

        if (!landedOrDead) {

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    switch (hit.collider.gameObject.name)
                    {
                        case "MedBay":
                            if (GetComponent<RoomTracking>().inMedBay)
                            {
                                //reset clicks & click timing tracker
                                clicksInRoom += 1;
                            }
                            else
                            {
                                playerObject.GetComponent<PlayerMovement>().target = medBayTarget;
                                ResetClickCounter();
                            }
                            break;
                        case "Engine":
                            if (GetComponent<RoomTracking>().inEngine)
                            {
                                //reset clicks & click timing tracker
                                clicksInRoom += 1;
                            }
                            else
                            {
                                playerObject.GetComponent<PlayerMovement>().target = engineTarget;
                                ResetClickCounter();
                            }
                            break;
                        case "Empty":
                            if (GetComponent<RoomTracking>().inEmpty)
                            {
                                //reset clicks & click timing tracker
                                clicksInRoom += 1;
                            }
                            else
                            {
                                playerObject.GetComponent<PlayerMovement>().target = emptyTarget;
                                ResetClickCounter();
                            }
                            break;
                        case "Cockpit":
                            if (GetComponent<RoomTracking>().inCockpit)
                            {
                                //reset clicks & click timing tracker
                                clicksInRoom += 1;
                            }
                            else
                            {
                                playerObject.GetComponent<PlayerMovement>().target = cockpitTarget;
                                ResetClickCounter();
                            }
                            break;
                    }
                }
            }
        }
    }

    public void ResetClickCounter()
    {
        clicksInRoom = 0;
        clickTimerActual = clickTimer;
    }

    public void ClickTimingTracker()
    {
        clickTimerActual -= Time.deltaTime;

        if(clickTimerActual <= 0)
        {
            if (clicksInRoom > 4)
            {
                //call double action for current room
                if (GetComponent<RoomTracking>().inMedBay)
                {
                    GameObject.Find("MedBay").GetComponent<MedBayScript>().ActionToTake(2);
                }else if (GetComponent<RoomTracking>().inEngine)
                {
                    GameObject.Find("Engine").GetComponent<EngineScript>().ActionToTake(2);
                }
                else if (GetComponent<RoomTracking>().inCockpit)
                {
                    GameObject.Find("Cockpit").GetComponent<CockpitScript>().ActionToTake(2);
                }else if (GetComponent<RoomTracking>().inEmpty)
                {
                    GameObject.Find("Empty").GetComponent<EmptyScript>().ActionToTake(2);
                }
            }
            else
            {
                //call action for current room
                if (GetComponent<RoomTracking>().inMedBay)
                {
                    GameObject.Find("MedBay").GetComponent<MedBayScript>().ActionToTake(1);
                }
                else if (GetComponent<RoomTracking>().inEngine)
                {
                    GameObject.Find("Engine").GetComponent<EngineScript>().ActionToTake(1);
                }
                else if (GetComponent<RoomTracking>().inCockpit)
                {
                    GameObject.Find("Cockpit").GetComponent<CockpitScript>().ActionToTake(1);
                }
                else if (GetComponent<RoomTracking>().inEmpty)
                {
                    GameObject.Find("Empty").GetComponent<EmptyScript>().ActionToTake(1);
                }
            }
            ResetClickCounter();
        }
    }
}
