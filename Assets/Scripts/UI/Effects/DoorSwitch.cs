using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class DoorSwitch : InteractibleObject
    {
        public Renderer button;
        public Material unpoweredButton;
        public Material poweredButton;
        public List<Door> doorsIControl = new List<Door>();


        public override void Interact()
        {
            Debug.Log("1");
            if (Interactible)
            {
                Debug.Log("2");
                foreach (Door door in doorsIControl)
                {
                    Debug.Log("3");
                    door.IsOpen = !door.IsOpen;
                }
            }
        }
    }
}