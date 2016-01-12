using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ContainerSlot : InventorySlot
{
    public Container container;


    public void SetMyContainerData()
    {
        container.selectedIngredient = ii.ingredient;
        int ingredientAmount = container.GetIngredientAmount(ii.ingredient);
        if (ingredientAmount < 5)
        {
            container.RemoveInventoryItem(ii.ingredient, 1);
            container.playerInventory.AddInventoryItem(ii.ingredient, 1);
        }
        else
        {
            container.transferModal.Open(ingredientAmount, true);
        }
    }


    public void Start()
    {
        equipbutton.gameObject.SetActive(false);
        transferButton.onClick.AddListener(SetMyContainerData);
    }
}
