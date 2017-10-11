using UnityEngine;
using System.Collections;

public class MiniEngineFire : MonoBehaviour {

    public GameObject gameController;

	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("GameController");
	}
	
	// Update is called once per frame
	void Update () {

        transform.localScale = new Vector3(gameController.GetComponent<UITracker>().currentSpeed * .1f, .5f, 1);
    }
}
