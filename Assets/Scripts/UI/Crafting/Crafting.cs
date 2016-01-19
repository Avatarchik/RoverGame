using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crafting : Menu
{
    public CraftingInfoPanel craftingInfoPanel;
    public Inventory inventory;

    public List<Recipe> recipes = new List<Recipe>();
    public List<CraftingSlot> craftingSlots = new List<CraftingSlot>();

    private bool canClose = true;

    public override void Open()
    {
        if(!isActive)
        {
            SelectCraftingSlot(craftingSlots[0]);
            base.Open();
        }
    }


    public override void Close()
    {
        if(isActive && canClose)
        {
            base.Close();
        }
    }


    public void SelectCraftingSlot(CraftingSlot craftingSlot)
    {
        foreach(CraftingSlot cs in craftingSlots)
        {
            cs.IsSelected = false;
        }

        craftingSlot.IsSelected = true;

        craftingInfoPanel.SelectedRecipe = craftingSlot.Item.recipe;
        craftingInfoPanel.craftButton.interactable = true;

        foreach (RecipePortion rp in craftingSlot.Item.recipe.requiredIngredients)
        {
            if (inventory.GetIngredientAmount(rp.ingredient) < rp.ingredientCount)
            {
                craftingInfoPanel.craftButton.interactable = false;
            }
        }
    }


    public void CraftItem()
    {
        foreach (RecipePortion rp in craftingInfoPanel.SelectedRecipe.requiredIngredients)
        {
            inventory.RemoveInventoryItem(rp.ingredient, rp.ingredientCount);
        }
        
        StartCoroutine(CraftingCoroutine());
    }


    public IEnumerator CraftingCoroutine()
    {
        canClose = false;
        craftingInfoPanel.craftButton.interactable = false;

        float desiredTime = 5f;
        float elapsedTime = 0f;

        while (elapsedTime < desiredTime)
        {
            craftingInfoPanel.harvestImage.fillAmount = (elapsedTime / desiredTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        craftingInfoPanel.harvestImage.fillAmount = 0f;

        InventoryIngredient newII = new InventoryIngredient();
        newII.ingredient = craftingInfoPanel.SelectedRecipe.craftedItem;
        newII.amount = 1;

        inventory.AddInventoryItem(newII.ingredient, newII.amount);
        if (!inventory.IsActive) inventory.Open();

        canClose = true;
        SelectCraftingSlot(craftingSlots[0]);
        SelectSlot();
    }


    private void SelectSlot()
    {
        SelectCraftingSlot(craftingSlots[0]);
    }


    private void Awake()
    {
        CraftingSlot.OnSelectCraftingSlot += SelectCraftingSlot;

        craftingInfoPanel.craftButton.onClick.AddListener(CraftItem);
    }
}
