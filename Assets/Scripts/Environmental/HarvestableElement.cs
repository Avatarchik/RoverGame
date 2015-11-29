using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HarvestableElement : InteractibleObject
{
    public Inventory playerInventory;
    public List<InventoryIngredient> inventoryIngredients = new List<InventoryIngredient>();
    
    private bool interactible = true;


    public override void OnMouseDown()
    {
        base.OnMouseDown();
    }

    public override void OnMouseEnter()
    {
        base.OnMouseEnter();
    }


    public override void OnMouseExit()
    {
        base.OnMouseExit();
    }


    public override void Interact()
    {
        if(interactible)
        {
            interactible = false;
            foreach (InventoryIngredient ii in inventoryIngredients)
            {
                playerInventory.AddInventoryItem(ii);
            }
        }

        Destroy(gameObject);
    }


    private void Awake()
    {
        if (playerInventory == null) playerInventory = GameObject.FindObjectOfType<Inventory>() as Inventory;
    }
}
