using UnityEngine;
using System.Collections;

public class EngineFireMovement : MonoBehaviour {

	// Can set this only when speed is changed in the future
	void Update () {

        float speedPercentage = Ship.currentSpeed / Ship.maxSpeed;
        transform.localScale = new Vector3(speedPercentage, 1, 1);
    }
}
