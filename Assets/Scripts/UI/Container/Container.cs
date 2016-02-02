using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Container : Menu
{
    public ContainerSlot containerSlotPrefab;
    public Ingredient selectedIngredient;
    public Transform InventorySlotContainer;
    public Button closeButton;

    public List<Ingredient> ingredientsInInventory = new List<Ingredient>();
    public List<ContainerSlot> containerSlots = new List<ContainerSlot>();

    private ContainerObject currentContainer;


    public override void Open()
    {
        base.Open();
    }


    public void Open(List<Ingredient> ingredients, ContainerObject container)
    {
        currentContainer = container;
        ingredientsInInventory = ingredients;
        InitializeInventorySlots();
        Open();
    }


    public override void Close()
    {
        base.Close();
    }


    public virtual int GetIngredientAmount(int ingredientId)
    {
        int count = 0;
        foreach (Ingredient i in ingredientsInInventory)
        {
            if (i.id == ingredientId) count++;
        }
        return count;
    }


    public virtual int GetIngredientAmount(Ingredient ingredient)
    {
        int count = 0;
        foreach (Ingredient i in ingredientsInInventory)
        {
            if (i.id == ingredient.id) count++;
        }
        return count;
    }


    public virtual void AddInventoryItem(Ingredient ingredient, int count)
    {
        for (int i = count; i > 0; i--)
        {
            ingredientsInInventory.Add(ingredient);
        }

        InitializeInventorySlots();
    }


    public virtual void RemoveInventoryItem(Ingredient ingredient, int count)
    {
        while (count > 0)
        {
            for (int i = 0; i < ingredientsInInventory.Count; i++)
            {
                if (ingredientsInInventory[i].id == ingredient.id)
                {
                    ingredientsInInventory.RemoveAt(i);
                    count--;
                    break;
                }
            }
        }

        InitializeInventorySlots();
    }


    public virtual void InitializeInventorySlots()
    {
        for (int i = containerSlots.Count - 1; i >= 0; i--)
        {
            Destroy(containerSlots[i].gameObject);
        }

        containerSlots.Clear();
        List<Ingredient> encounteredIngredients = new List<Ingredient>();
        foreach (Ingredient i in ingredientsInInventory)
        {
            if (!encounteredIngredients.Contains(i))
            {
                BuildInventorySlot(i, GetIngredientAmount(i));
                encounteredIngredients.Add(i);
            }
        }
    }


    public virtual void BuildInventorySlot(Ingredient ingredient, int count)
    {
        ContainerSlot newSlot = Instantiate(containerSlotPrefab) as ContainerSlot;
        newSlot.transform.SetParent(InventorySlotContainer);
        newSlot.transform.localScale = Vector3.one;

        newSlot.image.sprite = ingredient.image;
        newSlot.Amount = count;
        newSlot.ii.ingredient = ingredient;
        newSlot.ii.amount = count;

        containerSlots.Add(newSlot);
    }


    private void Update()
    {
        if(IsActive)
        {
            if (Input.GetKeyDown(KeyCode.E)) Close();
        }
    }


    private void Awake()
    {
        closeButton.onClick.AddListener(Close);
        InitializeInventorySlots();
    }
}