using UnityEngine;
using System.Collections;

public class InteractibleObject : MonoBehaviour
{
    public float detectionDistance = 1000f;
    public Renderer objectRenderer;
    public Color mouseOverColor = Color.green;
    public Color baseColor;


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
        gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", mouseOverColor);
        Debug.Log("mousing over");
    }


    public virtual void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", baseColor);
        Debug.Log("mousing out");
    }


    public virtual void Interact()
    {
        Debug.Log("interacting");
    }


    private void Awake()
    {
        objectRenderer.material.EnableKeyword("_EMISSION");
        baseColor = objectRenderer.material.GetColor("_EmissionColor");
    }
}
