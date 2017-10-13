using UnityEngine;
using System.Collections;

public class SetParent : MonoBehaviour {

    public GameObject parentObject;
    public Vector3 offset;

	// Use this for initialization
	void Awake () {
        SetParentObject();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        SetParentObject();
	}

    void SetParentObject()
    {
        transform.position = parentObject.transform.position + offset;
    }
}
