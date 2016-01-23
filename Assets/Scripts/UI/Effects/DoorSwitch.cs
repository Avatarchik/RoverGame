using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorSwitch : InteractibleObject
{
    public Renderer button;
    public Material unpoweredButton;
    public Material poweredButton;
    public List<Door> doorsIControl = new List<Door>();


    public override void Interact()
    {
        if(Interactible)
        {
            foreach (Door door in doorsIControl)
            {
                door.IsOpen = !door.IsOpen;
            }
        }
    }
}
