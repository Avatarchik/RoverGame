using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class BatterySlot : InteractibleObject
    {
        public Ingredient desiredObject;

        public List<GameObject> objectsToActivate = new List<GameObject>();
        public List<GameObject> objectsToDeactivate = new List<GameObject>();

        private bool hasItem;
        private Inventory playerInventory;

        private const string NEGATIVE_STRING = "Its missing a {0}.";
        private const string AFFIRMATIVE_STRING = "I already gave it a {0}.";


        public Inventory PlayerInventory
        {
            get { return (playerInventory != null) ? playerInventory : playerInventory = UIManager.GetMenu<Inventory>(); }
        }


        public bool HasItem
        {
            get { return true; }
        }


        public override void Interact()
        {
            if (Interactible)
            {
                if (PlayerInventory.GetIngredientAmount(desiredObject) > 0)
                {
                    PlayerInventory.RemoveInventoryItem(desiredObject, 1);

                    foreach(GameObject go in objectsToDeactivate)
                    {
                        go.SetActive(false);
                    }

                    foreach (GameObject go in objectsToActivate)
                    {
                        go.SetActive(true);
                    }

                    silhouetteSeen.SetActive(false);
                    silhouetteInteractible.SetActive(false);

                    hasItem = true;
                    interactible = false;
                }
                else
                {
                    StopAllCoroutines();
                    MessageMenu messageMenu = UIManager.Open<MessageMenu>();
                    
                    messageMenu.SetText(string.Format(NEGATIVE_STRING, desiredObject.displayName), 1);

                    StartCoroutine(CloseMessage());
                }
            }
        }


        private IEnumerator CloseMessage()
        {
            yield return new WaitForSeconds(3f);
            UIManager.Close<MessageMenu>();
        }
    }
}