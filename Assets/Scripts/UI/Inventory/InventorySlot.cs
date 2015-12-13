using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventorySlot : MonoBehaviour
{
    public InventoryIngredient ii;

    public Image image;
    public Text titleText;
    public Text descriptionText;
    public Text amountText;

    public Button equipbutton;

    public Inventory inventory;

    private bool equippable = false;
    private bool stackable = true;

    public int amount = 0;

    public int Amount
    {
        set
        {
            amount = value;
            amountText.text = amount + "";
        }
    }


    private void EquipCamera()
    {
        inventory.RemoveInventoryItem(ii.ingredient, ii.amount);
        CameraEquip.Equip(ii.ingredient.id);
    }


    private void Start()
    {
        stackable = ii.ingredient.stackable;
        equippable = ii.ingredient.equippable;
        
        equipbutton.gameObject.SetActive(equippable);
        amountText.gameObject.SetActive(stackable);
        equipbutton.onClick.AddListener(EquipCamera);
    }
}
