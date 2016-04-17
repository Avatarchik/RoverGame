using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class InteractibleActivation : InteractibleObject
    {
        public List<GameObject> objectsToActivate = new List<GameObject>();
        public List<GameObject> objectsToDeactivate = new List<GameObject>();


        public override void Interact()
        {
            base.Interact();

            if(Interactible)
            {
                foreach(GameObject go in objectsToActivate)
                {
                    go.SetActive(true);
                }

                foreach(GameObject go in objectsToDeactivate)
                {
                    go.SetActive(false);
                }
            }
        }
    }
}

