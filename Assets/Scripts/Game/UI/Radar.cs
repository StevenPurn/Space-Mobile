using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour {

    public GameObject parent;
    private RectTransform rectTrans;

	// Use this for initialization
	void Start () {
		if(parent == null)
        {
            parent = transform.parent.gameObject;
        }
        rectTrans = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        float xPos = (parent.GetComponent<RectTransform>().rect.width / Ship.maxSpeed) * Ship.currentSpeed;
        float yPos = (parent.GetComponent<RectTransform>().rect.height / Ship.maxRotation) * Ship.go.transform.rotation.z * -50;
        rectTrans.anchoredPosition = new Vector3(xPos, yPos, 0);
	}
}
