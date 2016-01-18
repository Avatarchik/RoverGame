using UnityEngine;
using System.Collections;

public class InteractibleObject : MonoBehaviour
{
    public float detectionDistance = 1000f;
    public Renderer objectRenderer;
    //public Material highlightMaterial;
    //public Material baseMaterial;
    public GameObject silhouette;
    public bool interactible = true;

    


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
        if (objectRenderer != null && interactible) silhouette.SetActive(true);//objectRenderer.material = highlightMaterial;
    }


    public virtual void OnMouseExit()
    {
        if (objectRenderer != null) silhouette.SetActive(false);//objectRenderer.material = baseMaterial;
    }


    public virtual void Interact()
    {
        Debug.Log("interacting");
    }


    private void Awake()
    {
       // if (objectRenderer != null) // baseMaterial = objectRenderer.material;
    }
}
