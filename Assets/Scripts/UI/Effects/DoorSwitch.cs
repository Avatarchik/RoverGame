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
        public ParticleSystem sparks;


        public override void Interact()
        {
            if (Interactible)
            {
                foreach (Door door in doorsIControl)
                {
                    sparks.Play();
                    door.IsOpen = !door.IsOpen;
                }
            }
        }
    }
}