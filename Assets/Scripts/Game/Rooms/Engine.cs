using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Engine : Rooms
{

    public override void RoomAction(int multiplier)
    {
        AnimationController.SetAnimation("EngineControl");
        EngineAction(multiplier);
    }

    public override void RoomUpdate()
    {
        //check for malfunctions
    }

    void EngineAction(int multiplier)
    {
        //Set ship speed
    }
}