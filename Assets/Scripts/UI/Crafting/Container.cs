using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Container : Inventory
{
    public Inventory playerInventory;

    public Button transferButton;

    public Ingredient selectedIngredient;
    public int transferAmount;

    public TransferModal transferModal;

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


    public void TransferItem()
    {
        
    }


    public override void Update()
    {
        // do nothing, literally nothing. you bastard
    }


    private void Awake()
    {
        playerInventory = GameObject.FindObjectOfType<Inventory>() as Inventory;
        transferButton.onClick.AddListener(TransferItem);
        InitializeInventorySlots();
    }
}
