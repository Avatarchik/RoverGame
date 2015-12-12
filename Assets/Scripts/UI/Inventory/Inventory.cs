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

    public List<InventoryIngredient> ingredientsInInventory = new List<InventoryIngredient>();

    private List<InventorySlot> inventorySlots = new List<InventorySlot>();

    public float Weight
    {
        get
        {
            float weight = 0f;

            foreach(InventoryIngredient ii in ingredientsInInventory)
            {
                weight += ii.amount * ii.ingredient.weight;
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


    public void InitializeInventorySlots()
    {
        for(int i = inventorySlots.Count -1; i >=0; i--)
        {
            Destroy(inventorySlots[i].gameObject);
        }

        inventorySlots.Clear();

        foreach(InventoryIngredient inventoryIngredient in ingredientsInInventory)
        {
            BuildInventorySlot(inventoryIngredient);
        }
    }


    public void BuildInventorySlot(InventoryIngredient inventoryIngredient)
    {
        InventorySlot newSlot = Instantiate(inventorySlotPrefab) as InventorySlot;
        newSlot.transform.SetParent(InventorySlotContainer);
        newSlot.transform.localScale = Vector3.one;
        newSlot.inventory = this;

        newSlot.titleText.text = inventoryIngredient.ingredient.displayName;
        newSlot.descriptionText.text = inventoryIngredient.ingredient.description;
        newSlot.image.sprite = inventoryIngredient.ingredient.image;
        newSlot.Amount = inventoryIngredient.amount;
        newSlot.equipbutton.gameObject.SetActive(false);
        newSlot.ii.ingredient = inventoryIngredient.ingredient;
        newSlot.ii.amount = inventoryIngredient.amount;

        inventorySlots.Add(newSlot);
    }


    public void AddInventoryItem(InventoryIngredient ii)
    {
        for (int i = 0; i < ingredientsInInventory.Count; i++)
        {
            if (ingredientsInInventory[i].ingredient.id == ii.ingredient.id)
            {
                ingredientsInInventory[i].amount += ii.amount;
                InitializeInventorySlots();
                return;
            }
        }

        ingredientsInInventory.Add(ii);
        InitializeInventorySlots();
    }


    public void RemoveInventoryItem(InventoryIngredient ii)
    {
        for(int i = 0; i < ingredientsInInventory.Count; i++)
        {
            if (ingredientsInInventory[i].ingredient.id == ii.ingredient.id)
            {
                ingredientsInInventory[i].amount -= ii.amount;

                if (ingredientsInInventory[i].amount == 0)
                {
                    ingredientsInInventory[i] = null;
                    ingredientsInInventory.RemoveAt(i);
                }
            }
        }

        InitializeInventorySlots();
    }


    public void RemoveIngredient(Ingredient ingredient, int amount)
    {
        for (int i = 0; i < ingredientsInInventory.Count; i++)
        {
            if (ingredientsInInventory[i].ingredient.id == ingredient.id)
            {
                ingredientsInInventory[i].amount -= amount;

                if (ingredientsInInventory[i].amount == 0)
                {
                    ingredientsInInventory[i] = null;
                    ingredientsInInventory.RemoveAt(i);
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
