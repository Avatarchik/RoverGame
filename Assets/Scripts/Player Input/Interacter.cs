using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class Interacter : MonoBehaviour
    {
        public float detectionDistance = 50f;
        public float interactDistance = 15f;

        private List<InteractibleObject> seenInteractibleObjects = new List<InteractibleObject>();
        private List<InteractibleObject> interactibleInteractibleObjects = new List<InteractibleObject>();


        public void Update()
        {
            //figure out what we are close enough to highlight
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, detectionDistance);

            List<InteractibleObject> interactibles = new List<InteractibleObject>();

            foreach (RaycastHit hit in hits)
            {
                InteractibleObject[] ios = hit.collider.gameObject.GetComponents<InteractibleObject>();
                interactibles.AddRange(ios);
            }
            SetSeenSilhouettes(interactibles);

            //figure out what we are close enough to interact with
            hits = Physics.RaycastAll(ray, interactDistance);
            interactibles.Clear();
            foreach (RaycastHit hit in hits)
            {
                InteractibleObject[] ios = hit.collider.gameObject.GetComponents<InteractibleObject>();
                interactibles.AddRange(ios);
            }
            SetInteractibleSilhouettes(interactibles);
        }


        public void SetSeenSilhouettes(List<InteractibleObject> currentLitObjects)
        {
            List<InteractibleObject> overlappingItems = new List<InteractibleObject>();
            List<InteractibleObject> uniqueOldItems = new List<InteractibleObject>();
            List<InteractibleObject> uniqueNewItems = new List<InteractibleObject>();

            foreach (InteractibleObject co in currentLitObjects)
            {
                if (seenInteractibleObjects.Contains(co))
                {
                    //its currently lit, and it was previously lit. DO NOTHING
                    overlappingItems.Add(co);
                }
                else
                {
                    //its currently list, but it wasnt lit before. TURN IT ON
                    co.HoverEnterSeen();
                    seenInteractibleObjects.Add(co);
                }
            }

            for (int i = seenInteractibleObjects.Count - 1; i >= 0; i--)
            {
                if (!currentLitObjects.Contains(seenInteractibleObjects[i]))
                {
                    //its hovered, but not currently lit. TURN IT OFF
                    if (seenInteractibleObjects[i] != null)
                    {
                        seenInteractibleObjects[i].HoverExitSeen();
                        seenInteractibleObjects.Remove(seenInteractibleObjects[i]);
                    }
                    else
                    {
                        seenInteractibleObjects.Remove(seenInteractibleObjects[i]);
                    }
                }
            }
        }


        public void SetInteractibleSilhouettes(List<InteractibleObject> currentLitObjects)
        {
            List<InteractibleObject> overlappingItems = new List<InteractibleObject>();
            List<InteractibleObject> uniqueOldItems = new List<InteractibleObject>();
            List<InteractibleObject> uniqueNewItems = new List<InteractibleObject>();

            foreach (InteractibleObject co in currentLitObjects)
            {
                if (interactibleInteractibleObjects.Contains(co))
                {
                    //its currently lit, and it was previously lit. DO NOTHING
                    overlappingItems.Add(co);
                }
                else
                {
                    //its currently list, but it wasnt lit before. TURN IT ON
                    co.HoverEnterInteractible();

                    interactibleInteractibleObjects.Add(co);
                }
            }

            for(int i = interactibleInteractibleObjects.Count - 1; i >= 0; i--)
            {
                if (!currentLitObjects.Contains(interactibleInteractibleObjects[i]))
                {
                    //its hovered, but not currently lit. TURN IT OFF
                    interactibleInteractibleObjects[i].HoverExitInteractible();
                    interactibleInteractibleObjects.Remove(interactibleInteractibleObjects[i]);
                }
            }

            foreach (InteractibleObject io in currentLitObjects)
            {
                if (Input.GetMouseButtonDown(0)) io.Interact();
            }
        }
    }
}