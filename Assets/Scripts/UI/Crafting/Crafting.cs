using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crafting : Menu
{
    public CraftingInfoPanel craftingInfoPanel;
    public Inventory inventory;

    public List<CraftingSlot> craftingSlots = new List<CraftingSlot>();

    public override void Open()
    {
        SelectCraftingSlot(craftingSlots[0]);
        base.Open();
    }


    public override void Close()
    {
        base.Close();
    }


    public void SelectCraftingSlot(CraftingSlot craftingSlot)
    {
        foreach(CraftingSlot cs in craftingSlots)
        {
            cs.IsSelected = false;
        }

        craftingSlot.IsSelected = true;

        craftingInfoPanel.SelectedRecipe = craftingSlot.Item.recipe;

        foreach(RecipePortion rp in craftingSlot.Item.recipe.requiredIngredients)
        {
            foreach(InventoryIngredient ii in inventory.ingredientsInInventory)
            {
                if(ii.ingredient.id == rp.ingredient.id)
                {
                    if (ii.amount < rp.ingredientCount)
                    {
                        craftingInfoPanel.craftButton.interactable = false;
                        return;
                    }
                }
            }
        }

        craftingInfoPanel.craftButton.interactable = true;
    }


    public void CraftItem()
    {
        foreach (RecipePortion rp in craftingInfoPanel.SelectedRecipe.requiredIngredients)
        {
            inventory.RemoveIngredient(rp.ingredient, rp.ingredientCount);
        }

        InventoryIngredient newII = new InventoryIngredient();
        newII.ingredient = craftingInfoPanel.SelectedRecipe.craftedItem;
        newII.amount = 1;

        inventory.AddInventoryItem(newII);
        if (!inventory.IsActive) inventory.Open();

        SelectCraftingSlot(craftingSlots[0]);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            if(!IsActive)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
    }


    private void Awake()
    {
        CraftingSlot.OnSelectCraftingSlot += SelectCraftingSlot;

        craftingInfoPanel.craftButton.onClick.AddListener(CraftItem);
    }
}
