using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Crafting : Menu
{
    public CraftingInfoPanel craftingInfoPanel;
    public Transform craftingSlotContainer;
    public Button closeButton;
    public CraftingSlot craftingSlotPrefab;

    public List<Recipe> recipes = new List<Recipe>();
    private List<CraftingSlot> craftingSlots = new List<CraftingSlot>();
    private PlayerStats playerStatsReference;

    private bool canClose = true;

    public PlayerStats PlayerStatsReference
    {
        get
        {
            if (playerStatsReference == null) playerStatsReference = GameManager.Get<PlayerStats>();
            if (playerStatsReference == null) playerStatsReference = GameObject.FindObjectOfType<PlayerStats>();

            return playerStatsReference;
        }
    }

    public override void Open()
    {
        if(!isActive)
        {
            InitializeSlots(recipes);
            base.Open();
        }
    }


    public void Open(List<Recipe> recipes)
    {
        if(!isActive)
        {
            List<Recipe> recipesToOpen = new List<Recipe> ();

            recipes.Clear();
            recipesToOpen.AddRange(recipes);
            recipesToOpen.AddRange(PlayerStatsReference.knownRecipes);
            Debug.Log(recipesToOpen.Count);
            recipesToOpen = SanitizeRecipes(recipesToOpen);

            recipes = recipesToOpen;

            Open();
        }
    }


    public List<Recipe> SanitizeRecipes(List<Recipe> recipesToSanitize)
    {
        List<Recipe> sanitizedRecipes = new List<Recipe> ();
        //TODO this is removing recipes it shouldnt?
        foreach(Recipe r in recipesToSanitize)
        {
            if (!sanitizedRecipes.Contains(r)) sanitizedRecipes.Add(r);
        }

        return recipesToSanitize;
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

        craftingInfoPanel.SelectedRecipe = craftingSlot.recipe;
        craftingInfoPanel.craftButton.interactable = true;

        foreach (RecipePortion rp in craftingSlot.recipe.requiredIngredients)
        {
            if (UIManager.GetMenu<Inventory>().GetIngredientAmount(rp.ingredient) < rp.ingredientCount)
            {
                craftingInfoPanel.craftButton.interactable = false;
            }
        }
    }


    public void CraftItem()
    {
        foreach (RecipePortion rp in craftingInfoPanel.SelectedRecipe.requiredIngredients)
        {
            UIManager.GetMenu<Inventory>().RemoveInventoryItem(rp.ingredient, rp.ingredientCount);
        }
        
        StartCoroutine(CraftingCoroutine());
    }


    public IEnumerator CraftingCoroutine()
    {
        canClose = false;
        craftingInfoPanel.craftButton.interactable = false;
        InventoryIngredient newII = new InventoryIngredient();
        newII.ingredient = craftingInfoPanel.SelectedRecipe.craftedItem;
        float desiredTime = 5f;
        float elapsedTime = 0f;

        while (elapsedTime < desiredTime)
        {
            craftingInfoPanel.craftingFillBar.fillAmount = (elapsedTime / desiredTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        craftingInfoPanel.craftingFillBar.fillAmount = 0f;
        
        newII.amount = 1;
        UIManager.GetMenu<Inventory>().AddInventoryItem(newII.ingredient, newII.amount);

        canClose = true;
        SelectCraftingSlot(craftingSlots[0]);
        SelectSlot();
    }


    private void InitializeSlots(List<Recipe> recipeList)
    {
        //TODO optomize this to reuse slots!
        foreach(CraftingSlot cs in craftingSlots)
        {
            Destroy(cs.gameObject);
        }
        craftingSlots.Clear();
        int i = 0;
        foreach(Recipe recipe in recipes)
        {
            Debug.Log(" creating slot at :" + i);
            CraftingSlot newCraftingSlot = Instantiate(craftingSlotPrefab) as CraftingSlot;
            newCraftingSlot.transform.SetParent(craftingSlotContainer);
            newCraftingSlot.titleText.text = recipe.displayName;
            newCraftingSlot.ingredient = recipe.craftedItem;
            newCraftingSlot.recipe = recipe;

            craftingSlots.Add(newCraftingSlot);
            i++;
        }

        if(craftingSlots.Count > 0) SelectCraftingSlot(craftingSlots[0]);
    }


    private void SelectSlot()
    {
        SelectCraftingSlot(craftingSlots[0]);
    }


    private void Awake()
    {
        CraftingSlot.OnSelectCraftingSlot += SelectCraftingSlot;
        closeButton.onClick.AddListener(Close);
        craftingInfoPanel.craftButton.onClick.AddListener(CraftItem);
    }
}
