using UnityEngine;
using System.Collections;

namespace Sol
{
    public class InteractibleObject : MonoBehaviour
    {
        public string objectName = "Container";

        public GameObject silhouetteInteractible;
        public GameObject silhouetteSeen;
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


        public virtual void HoverEnterSeen()
        {
            if(Interactible)
            {
                silhouetteSeen.SetActive(true);
            }
        }


        public void HoverEnterInteractible()
        {
            if(Interactible)
            {
                silhouetteInteractible.SetActive(true);
                if (objectName != "")
                {
                    MessageMenu messageMenu = UIManager.GetMenu<MessageMenu>();
                    messageMenu.Open(objectName);
                }
            }
        }


        public virtual void HoverExitSeen()
        {
            if(Interactible)
            {
                silhouetteSeen.SetActive(false);
            }
        }


        public virtual void HoverExitInteractible()
        {
            if(Interactible)
            {
                if(silhouetteInteractible != null) silhouetteInteractible.SetActive(false);
                UIManager.Close<MessageMenu>();
            }
        }


        public virtual void Interact()
        {
            if(Interactible)
            {
                Debug.Log("interacting");
                UIManager.Close<MessageMenu>();
                silhouetteSeen.SetActive(false);
                silhouetteInteractible.SetActive(false);
            }
        }
    }
}