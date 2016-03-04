using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class Interacter : MonoBehaviour
    {
        public float detectionDistance = 50f;
        public float highlightDistance = 15f;

        private List<InteractibleObject> hoveredInteractibleObjects = new List<InteractibleObject>();


        public void Update()
        {
            //figure out what we are close enough to highlight
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, highlightDistance);

            List<InteractibleObject> interactibles = new List<InteractibleObject>();

            foreach (RaycastHit hit in hits)
            {
                InteractibleObject[] ios = hit.collider.gameObject.GetComponents<InteractibleObject>();
                interactibles.AddRange(ios);
            }
            SetSilhouettes(interactibles);

            //figure out what we are close enough to interact with
            hits = Physics.RaycastAll(ray, detectionDistance);
            interactibles.Clear();
            foreach (RaycastHit hit in hits)
            {
                InteractibleObject[] ios = hit.collider.gameObject.GetComponents<InteractibleObject>();
                interactibles.AddRange(ios);
            }
            SetInteractibles(interactibles);
        }


        public void SetSilhouettes(List<InteractibleObject> currentLitObjects)
        {
            List<InteractibleObject> overlappingItems = new List<InteractibleObject>();
            List<InteractibleObject> uniqueOldItems = new List<InteractibleObject>();
            List<InteractibleObject> uniqueNewItems = new List<InteractibleObject>();

            foreach (InteractibleObject co in currentLitObjects)
            {
                if (hoveredInteractibleObjects.Contains(co))
                {
                    //its currently lit, and it was previously lit. DO NOTHING
                    overlappingItems.Add(co);
                }
                else
                {
                    //its currently list, but it wasnt lit before. TURN IT ON
                    co.HoverEnter();
                    hoveredInteractibleObjects.Add(co);
                }
            }

            for(int i = hoveredInteractibleObjects.Count - 1; i >= 0; i--)
            {
                if (!currentLitObjects.Contains(hoveredInteractibleObjects[i]))
                {
                    //its hovered, but not currently lit. TURN IT OFF
                    hoveredInteractibleObjects[i].HoverExit();
                    hoveredInteractibleObjects.Remove(hoveredInteractibleObjects[i]);
                }
            }
        }


        public void SetInteractibles(List<InteractibleObject> currentLitObjects)
        {
            foreach (InteractibleObject io in currentLitObjects)
            {
                if (Input.GetMouseButtonDown(0)) io.Interact();
            }
        }
    }
}