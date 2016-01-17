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
        if(PlayerInventory.GetIngredientAmount(desiredObject) > 0)
        {
            PlayerInventory.RemoveInventoryItem(desiredObject, 1);
            doorSwitch.interactible = true;
            podAnimator.hazardLight.gameObject.SetActive(true);
            hasItem = true;
        }
    }
}
