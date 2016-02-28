using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interacter : MonoBehaviour
{
    public float detectionDistance = 50f;

    private List<InteractibleObject> hoveredInteractibleObjects = new List<InteractibleObject>();


    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, detectionDistance);

        List<InteractibleObject> interactibles = new List<InteractibleObject>();

        foreach (RaycastHit hit in hits)
        {
            InteractibleObject[] ios = hit.collider.gameObject.GetComponents<InteractibleObject>();
            interactibles.AddRange(ios);
        }

        SetInteractibleObjects(interactibles);
    }


    public void SetInteractibleObjects(List<InteractibleObject> currentLitObjects)
    {
        foreach (InteractibleObject inob in hoveredInteractibleObjects)
        {
            inob.HoverExit();
        }
        hoveredInteractibleObjects.Clear();
        

        foreach(InteractibleObject io in currentLitObjects)
        {
            if(!hoveredInteractibleObjects.Contains(io))
            {
                io.HoverEnter();
                hoveredInteractibleObjects.Add(io);
                if (Input.GetMouseButtonDown(0)) io.Interact();
            }
        }
    }
}
