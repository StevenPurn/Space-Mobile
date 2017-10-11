using UnityEngine;
using System.Collections;

public class PlayAnimationOnWake : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Animation>().Play();
        Destroy(this);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
