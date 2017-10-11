using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITracker : MonoBehaviour {

    public int currentSpeed = 10;
    public int maxSpeed = 20;
    public float positionOffset = .15f;
    public GameObject speedUIElement;
    public GameObject[] gameObjects;
    public GameObject instantiatedUI;

    public void SpeedChange(int change)
    {
        GameObject.Find("Engine").GetComponent<EngineScript>().UpdateShipSpeed(change);
        currentSpeed = GameObject.Find("Engine").GetComponent<EngineScript>().shipSpeed;

        UpdateUI();
    }

    void DestroyAllObjects(string tagToDestroy)
    {
        gameObjects = GameObject.FindGameObjectsWithTag(tagToDestroy);

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }

    public void  UpdateUI()
    {
        DestroyAllObjects("speedUI");

        for (int i = 0; i < GameObject.Find("Engine").GetComponent<EngineScript>().shipSpeed/2; i++)
        {
            var speedBGTransform = GameObject.Find("SpeedBG").transform;
            instantiatedUI = (GameObject)Instantiate(speedUIElement, new Vector3(speedBGTransform.position.x, speedBGTransform.position.y + ((float)i * positionOffset), 0), speedBGTransform.rotation);
            instantiatedUI.transform.SetParent(speedBGTransform);
            //instantiatedUI.transform.localPosition = new Vector3(speedBGTransform.position.x, speedBGTransform.position.y + ((float)i * positionOffset),0);
            instantiatedUI.transform.localScale = speedBGTransform.localScale * .8f;
            instantiatedUI.transform.name = "SpeedIndicator_" + i;

            var positionTracker = new Vector2(0, (float)i * positionOffset);
            instantiatedUI.GetComponent<RectTransform>().localPosition = positionTracker;
        }
    }

	// Use this for initialization
	void Start () {
        SpeedChange(0);
	}
}
