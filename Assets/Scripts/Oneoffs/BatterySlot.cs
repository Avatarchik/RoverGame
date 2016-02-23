using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class BatterySlot : InteractibleObject
    {
        public Ingredient desiredObject;
        public PodAnimator podAnimator;
        public DoorSwitch doorSwitch;
        public GameObject batteryModel;

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
                    Debug.Log("should have item!!");
                    PlayerInventory.RemoveInventoryItem(desiredObject, 1);
                    batteryModel.SetActive(true);
                    doorSwitch.interactible = true;
                    podAnimator.hazardLight.gameObject.SetActive(true);
                    hasItem = true;
                    interactible = false;
                }
                else
                {
                    StopAllCoroutines();
                    MessageMenu messageMenu = UIManager.Open<MessageMenu>();
                    if (HasItem)
                    {
                        messageMenu.SetText(string.Format(AFFIRMATIVE_STRING, desiredObject.displayName));
                    }
                    else
                    {
                        messageMenu.SetText(string.Format(NEGATIVE_STRING, desiredObject.displayName));
                    }

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