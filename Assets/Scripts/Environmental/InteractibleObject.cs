using UnityEngine;
using System.Collections;

public class InteractibleObject : MonoBehaviour
{
    public float detectionDistance = 1000f;
    public string objectName = "Container";
    public Renderer objectRenderer;

    public GameObject silhouette;
    public bool interactible = true;

    public bool Interactible
    {
        get { return interactible && UIManager.PlayerStatsInstance.movementEnabled == 0; }
    }


    public virtual void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, detectionDistance);

        foreach(RaycastHit hit in hits)
        {
            if(hit.collider.gameObject == gameObject)
            {
                Interact();
            }
        }
    }


    public virtual void OnMouseEnter()
    {
        if (objectRenderer != null && Interactible)
        {
            silhouette.SetActive(true);
            UIManager.MessageMenuInstance.Open(objectName);
        }
    }


    public virtual void OnMouseExit()
    {
        if (objectRenderer != null)
        {
            silhouette.SetActive(false);
            UIManager.MessageMenuInstance.Close();
        }
    }


    public virtual void Interact()
    {
        Debug.Log("interacting");
        silhouette.SetActive(false);
    }
}
