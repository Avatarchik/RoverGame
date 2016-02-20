using UnityEngine;
using System.Collections;

namespace Sol
{
    public class InteractibleObject : MonoBehaviour
    {
        public string objectName = "Container";

        public GameObject silhouette;
        public bool interactible = true;

        private PlayerStats playerStats;

        private PlayerStats PlayerStats
        {
            get
            {
                if (playerStats == null) playerStats = GameManager.Get<PlayerStats>();
                if (playerStats == null) playerStats = GameObject.FindObjectOfType<PlayerStats>();
                return playerStats;
            }
        }

        public bool Interactible
        {
            //TODO we need a clearer reference to the players stats!
            get { return interactible && PlayerStats.movementEnabled == 0; }
        }


        public virtual void HoverEnter()
        {
            if (Interactible)
            {
                SetSilhouette(true);
                if (objectName != "")
                {
                    MessageMenu messageMenu = UIManager.GetMenu<MessageMenu>();
                    if (!messageMenu) messageMenu = GameObject.FindObjectOfType<MessageMenu>();
                    messageMenu.Open(objectName);
                }
            }
        }


        public virtual void HoverExit()
        {
            SetSilhouette(false);
            UIManager.Close<MessageMenu>();
        }


        public virtual void Interact()
        {
            Debug.Log("interacting");
            SetSilhouette(false);
        }


        protected virtual void SetSilhouette(bool b)
        {
            if (silhouette != null) silhouette.SetActive(b);
        }
    }
}