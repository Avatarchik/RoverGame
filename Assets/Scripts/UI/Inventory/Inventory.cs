using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : Menu
{
    public InventorySlot inventorySlotPrefab;
    public Transform InventorySlotContainer;
    public Text weightValue;

    public PlayerStats playerStats;

    public List<Ingredient> ingredientsInInventory = new List<Ingredient>();

    private List<InventorySlot> inventorySlots = new List<InventorySlot>();

    public float Weight
    {
        get
        {
            float weight = 0f;

            foreach(Ingredient i in ingredientsInInventory)
            {
                weight += i.weight;
            }

            return weight;
        }
    }


    public override void Open()
    {
        base.Open();
    }


    public override void Close()
    {
        base.Close();
    }


    public int GetIngredientAmount(int ingredientId)
    {
        int count = 0;
        foreach(Ingredient i in ingredientsInInventory)
        {
            if (i.id == ingredientId) count++;
        }
        return count;
    }


    public int GetIngredientAmount(Ingredient ingredient)
    {
        int count = 0;
        foreach (Ingredient i in ingredientsInInventory)
        {
            if (i.id == ingredient.id) count++;
        }
        return count;
    }


    public void InitializeInventorySlots()
    {
        for(int i = inventorySlots.Count -1; i >=0; i--)
        {
            Destroy(inventorySlots[i].gameObject);
        }

        inventorySlots.Clear();
        List<Ingredient> encounteredIngredients = new List<Ingredient> ();
        foreach(Ingredient i in ingredientsInInventory)
        {
            if(!encounteredIngredients.Contains(i))
            {
                BuildInventorySlot(i, GetIngredientAmount(i));
                encounteredIngredients.Add(i);
            }
        }
    }


    public void BuildInventorySlot(Ingredient ingredient, int count)
    {
        InventorySlot newSlot = Instantiate(inventorySlotPrefab) as InventorySlot;
        newSlot.transform.SetParent(InventorySlotContainer);
        newSlot.transform.localScale = Vector3.one;
        newSlot.inventory = this;

        newSlot.titleText.text = ingredient.displayName;
        newSlot.descriptionText.text = ingredient.description;
        newSlot.image.sprite = ingredient.image;
        newSlot.Amount = count;
        newSlot.equipbutton.gameObject.SetActive(false);
        newSlot.ii.ingredient = ingredient;
        newSlot.ii.amount = count;

        inventorySlots.Add(newSlot);
    }


    public void AddInventoryItem(Ingredient ingredient, int count)
    {
        for(int i = count; i > 0; i--)
        {
            ingredientsInInventory.Add(ingredient);
        }

        InitializeInventorySlots();
    }


    public void RemoveInventoryItem(Ingredient ingredient, int count)
    {
        while(count > 0)
        {
            for(int i = 0; i < ingredientsInInventory.Count; i++)
            {
                if(ingredientsInInventory[i].id == ingredient.id)
                {
                    ingredientsInInventory.RemoveAt(i);
                    count--;
                }
            }
        }

        InitializeInventorySlots();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!IsActive)
            {
                Open();
            }
            else
            {
                Close();
            }
        }

        weightValue.text = playerStats.Weight + " kg";
    }


    private void Awake()
    {
        InitializeInventorySlots();
    }
}
