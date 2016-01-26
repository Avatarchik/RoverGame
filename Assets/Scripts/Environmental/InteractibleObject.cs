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
        //TODO we need a clearer reference to the players stats!
        get { return interactible && GameManager.Get<PlayerStats>().movementEnabled == 0; }
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
            MessageMenu messageMenu = UIManager.Open<MessageMenu>();
            messageMenu.messageText.text = objectName;
        }
    }


    public virtual void OnMouseExit()
    {
        if (objectRenderer != null)
        {
            silhouette.SetActive(false);
            UIManager.Close<MessageMenu>();
        }
    }


    public virtual void Interact()
    {
        Debug.Log("interacting");
        silhouette.SetActive(false);
    }
}
