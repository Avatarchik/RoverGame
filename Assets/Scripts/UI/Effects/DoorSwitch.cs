using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorSwitch : InteractibleObject
{
    public List<Door> doorsIControl = new List<Door>();


    public override void Interact()
    {
        foreach(Door door in doorsIControl)
        {
            door.IsOpen = !door.IsOpen;
        }
    }
}
