using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ContainerSlot : InventorySlot
{
    public Container container;


    public void SetMyContainerData()
    {
        container.selectedIngredient = ii.ingredient;
        container.transferModal.Open(container.GetIngredientAmount(ii.ingredient), true);
    }


    public void Start()
    {
        equipbutton.gameObject.SetActive(false);
        transferButton.onClick.AddListener(SetMyContainerData);
    }
}
