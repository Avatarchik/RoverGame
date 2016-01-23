using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BatterySlot : InteractibleObject
{
    public Ingredient desiredObject;
    public PodAnimator podAnimator;
    public DoorSwitch doorSwitch;

    private bool hasItem;
    private Inventory playerInventory;

    private const string MESSAGE_STRING_1 = "Its missing a {0}.";
    private const string MESSAGE_STRING_2 = "I already gave it a {0}.";


    public Inventory PlayerInventory
    {
        get { return (playerInventory != null) ? playerInventory : playerInventory = GameObject.FindObjectOfType <Inventory>() as Inventory; }
    }


    public bool HasItem
    {
        get { return hasItem; }
    }


    public override void Interact()
    {
        if(Interactible)
        {
            if (PlayerInventory.GetIngredientAmount(desiredObject) > 0)
            {
                PlayerInventory.RemoveInventoryItem(desiredObject, 1);
                doorSwitch.interactible = true;
                podAnimator.hazardLight.gameObject.SetActive(true);
                hasItem = true;
            }
            else
            {
                StopAllCoroutines();
                if(HasItem)
                {
                    UIManager.MessageMenuInstance.Open(string.Format(MESSAGE_STRING_2, desiredObject.displayName));
                }
                else
                {
                    UIManager.MessageMenuInstance.Open(string.Format(MESSAGE_STRING_1, desiredObject.displayName));
                }
                
                StartCoroutine(CloseMessage());
            }
        }
    }


    private IEnumerator CloseMessage()
    {
        yield return new WaitForSeconds(3f);
        UIManager.MessageMenuInstance.Close();
    }
}
