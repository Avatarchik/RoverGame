using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransferToolTip : ToolTip
{
    public Button transferButton;

    private int currentAmount = 0;
    private InventoryIngredient inventoryIngredient = null;
    private bool toInventory = false;



    public override void Open()
    {
        base.Open();
    }


    public void Open(InventoryIngredient ii, bool transferToInventory)
    {
        inventoryIngredient = ii;
        toInventory = transferToInventory; 
    }


    public void OpenTransferModal()
    {
        Inventory inventory = UIManager.GetMenu<Inventory>();
        Container container = UIManager.GetMenu<Container>();
        TransferModal transferModal = UIManager.GetMenu<TransferModal>();
        int amount = 0;

        if (toInventory)
        {
            //we need to add to the inventory
            amount = container.GetIngredientAmount(inventoryIngredient.ingredient);

            if (amount > 5)
            {
                //open the modal, theres a bunch of things.
                transferModal = UIManager.GetMenu<TransferModal>();
                transferModal.Open(amount, inventoryIngredient, toInventory);

                Close();
            }
            else
            {
                //we just need to throw one over
                inventory.AddInventoryItem(inventoryIngredient.ingredient, 1);
                container.RemoveInventoryItem(inventoryIngredient.ingredient, 1);
            }
        }
        else
        {
            //we need to add to the container
            amount = inventory.GetIngredientAmount(inventoryIngredient.ingredient);
            
            if(amount > 5)
            {
                //open the modal, theres a bunch of things.
                transferModal = UIManager.GetMenu<TransferModal>();
                transferModal.Open(amount, inventoryIngredient, toInventory);

                Close();
            }
            else
            {
                //we just need to throw one over
                inventory.RemoveInventoryItem(inventoryIngredient.ingredient, 1);
                container.AddInventoryItem(inventoryIngredient.ingredient, 1);
            }
        }        
    }


    protected override void Awake()
    {
        transferButton.onClick.AddListener(OpenTransferModal);
        closeButton.onClick.AddListener(Close);
    }
}
