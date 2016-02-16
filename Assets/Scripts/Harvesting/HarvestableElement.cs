using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HarvestableElement : InteractibleObject
{
    public List<InventoryIngredient> inventoryIngredients = new List<InventoryIngredient>();

    public override void Interact()
    {
        if(interactible)
        {
            interactible = false;
            foreach (InventoryIngredient ii in inventoryIngredients)
            {
                UIManager.GetMenu<Inventory>().AddInventoryItem(ii.ingredient, ii.amount);
            }
        }

        Destroy(gameObject);
    }
}
