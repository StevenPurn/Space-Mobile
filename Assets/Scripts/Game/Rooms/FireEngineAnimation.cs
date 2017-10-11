using UnityEngine;
using System.Collections;

public class FireEngineAnimation : MonoBehaviour {

    public Sprite[] fireAnimations;
    public int animationIndex;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        animationIndex = Random.Range(0, 3);

        GetComponent<SpriteRenderer>().sprite = fireAnimations[animationIndex];
	}
}
