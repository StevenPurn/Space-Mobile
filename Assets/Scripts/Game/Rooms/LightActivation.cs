using UnityEngine;
using System.Collections;

public class LightActivation : MonoBehaviour {

    public GameObject gameController;
    public GameObject cockpitLight;
    public GameObject medBayLight;
    public GameObject engineLight;
    public GameObject emptyLight;
    public GameObject speedUI;

    public Sprite greenOff;
    public Sprite greenOn;
    public Sprite redOff;
    public Sprite redOn;

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.Find("GameController");
        engineLight = GameObject.Find("EngineLight");
        medBayLight = GameObject.Find("MedBayLight");
        cockpitLight = GameObject.Find("CockpitLight");
        emptyLight = GameObject.Find("EmptyLight");
        speedUI = GameObject.Find("MainSpeedUI");

        cockpitLight.GetComponent<Light>().enabled = false;
        engineLight.GetComponent<Light>().enabled = false;
        medBayLight.GetComponent<Light>().enabled = false;
        emptyLight.GetComponent<Light>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateMedBayLight();
        UpdateEmptyLight();
        UpdateEngineLight();
        UpdateCockpitLight();
    }

    public void UpdateMedBayLight()
    {
        if (gameController.GetComponent<RoomTracking>().inMedBay)
        {
            medBayLight.GetComponent<Light>().enabled = true;
            if (gameController.GetComponent<ShipTracking>().fireInMedBay)
            {
                medBayLight.GetComponent<SpriteRenderer>().sprite = redOn;
            }
            else
            {
                medBayLight.GetComponent<SpriteRenderer>().sprite = greenOn;
            }
        }
        else if (gameController.GetComponent<ShipTracking>().fireInMedBay)
        {
            medBayLight.GetComponent<SpriteRenderer>().sprite = redOff;
        }
        else
        {
            medBayLight.GetComponent<SpriteRenderer>().sprite = greenOff;
        }
    }

    public void UpdateEngineLight()
    {
        if (gameController.GetComponent<RoomTracking>().inEngine)
        {

            engineLight.GetComponent<Light>().enabled = true;
            if (gameController.GetComponent<ShipTracking>().fireInEngine || GameObject.Find("Engine").GetComponent<EngineScript>().currentLevel <= 0)
            {
                ShowEngineUI(false);
                engineLight.GetComponent<SpriteRenderer>().sprite = redOn;
            }
            else
            {
                ShowEngineUI(true);
                engineLight.GetComponent<SpriteRenderer>().sprite = greenOn;
            }
        }
        else if (gameController.GetComponent<ShipTracking>().fireInEngine || GameObject.Find("Engine").GetComponent<EngineScript>().currentLevel <= 0)
        {
            ShowEngineUI(false);
            engineLight.GetComponent<SpriteRenderer>().sprite = redOff;
        }
        else
        {
            ShowEngineUI(false);
            engineLight.GetComponent<SpriteRenderer>().sprite = greenOff;
        }
    }

    public void UpdateEmptyLight()
    {
        if (gameController.GetComponent<RoomTracking>().inEmpty)
        {
            emptyLight.GetComponent<Light>().enabled = true;
            if (gameController.GetComponent<ShipTracking>().fireInEmpty)
            {
                emptyLight.GetComponent<SpriteRenderer>().sprite = redOn;
            }
            else
            {
                emptyLight.GetComponent<SpriteRenderer>().sprite = greenOn;
            }
        }
        else if (gameController.GetComponent<ShipTracking>().fireInEmpty)
        {
            emptyLight.GetComponent<SpriteRenderer>().sprite = redOff;
        }
        else
        {
            emptyLight.GetComponent<SpriteRenderer>().sprite = greenOff;
        }
    }

    public void UpdateCockpitLight()
    {
        if (gameController.GetComponent<RoomTracking>().inCockpit)
        {
            cockpitLight.GetComponent<Light>().enabled = true;
            if (gameController.GetComponent<ShipTracking>().fireInCockpit)
            {
                cockpitLight.GetComponent<SpriteRenderer>().sprite = redOn;
            }
            else
            {
                cockpitLight.GetComponent<SpriteRenderer>().sprite = greenOn;
            }
        }
        else if (gameController.GetComponent<ShipTracking>().fireInCockpit)
        {
            cockpitLight.GetComponent<SpriteRenderer>().sprite = redOff;
        }
        else
        {
            cockpitLight.GetComponent<SpriteRenderer>().sprite = greenOff;
        }
    }

    public void ShowEngineUI(bool show)
    {
        speedUI.SetActive(show);
    }
}
