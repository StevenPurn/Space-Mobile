using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooms : MonoBehaviour {

    public bool playerInRoom = false;
    public bool playerInPosition = false;
    public Fire fire;
    public GameObject roomPosition;

    [SerializeField]
    private Light roomLight;


    // Use this for initialization
    void Start () {
		if(roomLight == null)
        {
            roomLight = GetComponentInChildren<Light>();
        }

        if(fire != null)
        {
            fire.gameObject.SetActive(false);
        }
        GameControl.rooms.Add(this);
        Init();
	}
	
    //Use as start to avoid any issues with overriding
    public virtual void Init()
    {

    }

	// Update is called once per frame
	void Update () {
        if (playerInRoom)
        {
            roomLight.gameObject.SetActive(true);
            ActivateAction(1);
        }
        else
        {
            roomLight.gameObject.SetActive(false);
        }

        RoomUpdate();
	}

    public virtual void RoomUpdate()
    {

    }

    public void SpawnFire()
    {
        fire.gameObject.SetActive(true);
        fire.isActive = true;
    }

    public void ActivateAction(int multiplier)
    {
        if (fire.gameObject.activeSelf)
        {
            ExtinguishFire(multiplier);
        }
        else
        {
            RoomAction(multiplier);
        }
    }

    public virtual void RoomAction(int multiplier)
    {
        AnimationController.SetAnimation("Idle");
    }

    public void ExtinguishFire(int multiplier)
    {
        //Eventually need to account for the multiplier
        AnimationController.SetAnimation("FireFighting");
    }
}
