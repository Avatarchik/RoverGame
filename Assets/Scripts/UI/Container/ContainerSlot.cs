using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ContainerSlot : InventorySlot
{
    public void SetMyContainerData()
    {
        Container container = UIManager.GetMenu<Container>();
        container.selectedIngredient = ii.ingredient;
        int ingredientAmount = container.GetIngredientAmount(ii.ingredient);
        if (ingredientAmount < 5)
        {
            container.RemoveInventoryItem(ii.ingredient, 1);
            UIManager.GetMenu<Inventory>().AddInventoryItem(ii.ingredient, 1);
        }
        else
        {
            container.root.SetActive(false);
            UIManager.GetMenu<Inventory>().root.SetActive(false);
            TransferModal transferModal = UIManager.GetMenu<TransferModal>();
            transferModal.Open(ingredientAmount, true);
        }
    }





    private void OpenTransferToolTip()
    {
        TransferToolTip toolTip = UIManager.Open<TransferToolTip>();
        if (toolTip == null) toolTip = GameObject.FindObjectOfType<TransferToolTip>();
        toolTip.Open(ii.ingredient);
    }
}
