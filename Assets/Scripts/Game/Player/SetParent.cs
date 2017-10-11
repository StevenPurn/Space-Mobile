using UnityEngine;
using System.Collections;

public class SetParent : MonoBehaviour {

    public GameObject parentObject;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = parentObject.transform.position + offset;
	}
}
