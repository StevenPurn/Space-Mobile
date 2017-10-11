using UnityEngine;
using System.Collections;

public class CameraFollowShip : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(0,GameObject.Find("Ship").transform.position.y + 1, -10);

        if( transform.position.y < -34)
        {
            transform.position = new Vector3(0, -34, -10);
        }
	}
}
