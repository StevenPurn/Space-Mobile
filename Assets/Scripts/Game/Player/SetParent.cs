using UnityEngine;
using System.Collections;

public class SetParent : MonoBehaviour {

    public GameObject parentObject;
    public Vector3 offset;

	// Use this for initialization
	void Awake () {
        SetPosition();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        SetPosition();
	}

    void SetPosition()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(parentObject.transform.position);
        Quaternion rot = GameObject.Find("Ship").transform.rotation;
        transform.position = pos + offset;
        transform.rotation = rot;
    }
}
