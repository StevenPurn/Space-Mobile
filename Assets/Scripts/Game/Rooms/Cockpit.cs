using UnityEngine;
using System.Collections;

public class Cockpit : Rooms
{

    public override void RoomUpdate()
    {
        //Need to fix this to lower slowly and raise quickly
        bool controlled = playerInPosition && !fire.isActive;
        Ship.RotateShip(controlled, 1);
    }

    public override void RoomAction(int multiplier)
    {
        AnimationController.SetAnimation("CockpitNav");
    }
}