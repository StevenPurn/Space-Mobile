using UnityEngine;
using System.Collections;

public class ShipBlip : MonoBehaviour {

    public GameObject engineScript;
    public Vector2 positionTracker;
    public GameObject gameController;
    public float xValue;

	// Use this for initialization
	void Start () {
        engineScript = GameObject.Find("Engine");
        gameController = GameObject.Find("GameController");
	}
	
	// Update is called once per frame
	void Update () {

        if(engineScript.GetComponent<EngineScript>().shipSpeed <= 5)
        {
            xValue = -77.4f + (engineScript.GetComponent<EngineScript>().shipSpeed-1) * 15.48f;
        }
        else if(engineScript.GetComponent<EngineScript>().shipSpeed > 5)
        {
            xValue = (engineScript.GetComponent<EngineScript>().shipSpeed - 5) * 15.48f;
        }

        positionTracker = new Vector2(xValue, gameController.GetComponent<ShipTracking>().currentRotationRate * -5.1f);
        GetComponent<RectTransform>().localPosition = positionTracker; 
	}
}
