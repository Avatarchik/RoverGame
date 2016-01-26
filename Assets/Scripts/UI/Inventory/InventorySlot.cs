using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventorySlot : MonoBehaviour
{
    public InventoryIngredient ii;

    public Image image;
    public Text amountText;

    private bool equippable = false;
    private bool stackable = true;
    private Container container;

    public int amount = 0;

    public int Amount
    {
        set
        {
            amount = value;
            amountText.text = amount + "";
        }
    }


    public void SetContainerData()
    {
        Inventory inventory = UIManager.GetMenu<Inventory>();
        if (container == null) container = GameObject.FindObjectOfType<Container>() as Container;
        container.selectedIngredient = ii.ingredient;
        int ingredientAmount = inventory.GetIngredientAmount(ii.ingredient);
        if (ingredientAmount < 5)
        {
            inventory.RemoveInventoryItem(ii.ingredient, 1);
            container.AddInventoryItem(ii.ingredient, 1);
        }
        else
        {
            inventory.root.SetActive(false);
            container.root.SetActive(false);
            container.transferModal.Open(ingredientAmount, false);
        }
    }


    private void EquipCamera()
    {
        Inventory inventory = UIManager.GetMenu<Inventory>();
        inventory.RemoveInventoryItem(ii.ingredient, ii.amount);
        CameraEquip.Equip(ii.ingredient.id);
    }


    private void Awake()
    {

    }
}
