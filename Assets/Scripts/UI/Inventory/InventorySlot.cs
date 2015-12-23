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
    public Button transferButton;

    public Inventory inventory;

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
        if (container == null) container = GameObject.FindObjectOfType<Container>() as Container;
        container.selectedIngredient = ii.ingredient;
        container.transferModal.Open(inventory.GetIngredientAmount(ii.ingredient), false);

        Debug.Log("this happened?");
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
        transferButton.onClick.AddListener(SetContainerData);
    }
}
