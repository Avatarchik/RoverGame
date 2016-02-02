using UnityEngine;
using System.Collections;

public class InteractibleObject : MonoBehaviour
{
    public float detectionDistance = 1000f;
    public string objectName = "Container";
    public Renderer objectRenderer;

    public GameObject silhouette;
    public bool interactible = true;

    private PlayerStats playerStats;

    private PlayerStats PlayerStats
    {
        get
        {
            if(playerStats == null) playerStats = GameManager.Get<PlayerStats>();
            if (playerStats == null) playerStats = GameObject.FindObjectOfType<PlayerStats>();
            return playerStats;
        }
    }

    public bool Interactible
    {
        //TODO we need a clearer reference to the players stats!
        get { return interactible && PlayerStats.movementEnabled == 0; }
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
            MessageMenu messageMenu = UIManager.GetMenu<MessageMenu>();
            if (!messageMenu) messageMenu = GameObject.FindObjectOfType<MessageMenu>();
            messageMenu.Open(objectName);
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
