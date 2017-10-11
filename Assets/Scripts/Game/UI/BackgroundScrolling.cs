using UnityEngine;
using System.Collections;

public class BackgroundScrolling : MonoBehaviour {

    public float scrollSpeed = 0f;
    public int shipSpeed;
    public float backgroundScrollSpeed;

	// Use this for initialization
	void Start () {
	    if(scrollSpeed == 0)
        {
            scrollSpeed = 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
        shipSpeed = GameObject.Find("Engine").GetComponent<EngineScript>().shipSpeed;

        backgroundScrollSpeed = Mathf.Repeat(shipSpeed * scrollSpeed * Time.time, 2f) -1;

        GetComponent<Renderer>().sharedMaterial.mainTextureOffset = new Vector2 (-backgroundScrollSpeed, 0f);
	}
}
