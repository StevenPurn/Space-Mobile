using UnityEngine;
using System.Collections;

public class ShipSpriteChanger : MonoBehaviour {

    public Sprite destroyedShip;
    public GameObject ship;
    public GameObject gameController;

    // Use this for initialization
    void Start () {
        ship = GameObject.Find("ship_1");
        gameController = GameObject.Find("GameController");
	}
	

    void OnTriggerEnter2D(Collider2D other)
    {
        //Or speed is over allowable rate, speed is tracked some other crazy place
        gameController.GetComponent<ShipTracking>().shipMoving = false;
        if (gameController.GetComponent<ShipTracking>().currentRotationRate > 0)
        {
            gameController.GetComponent<ShipTracking>().CrashLanding();

        }else if(gameController.GetComponent<ShipTracking>().currentRotationRate < 0)
        {
            gameController.GetComponent<ShipTracking>().SafeLanding();
        }
    }
}
