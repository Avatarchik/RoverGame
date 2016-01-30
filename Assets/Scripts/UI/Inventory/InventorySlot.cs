using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventorySlot : MonoBehaviour
{
    public InventoryIngredient ii;

    public Image image;
    public Text amountText;
    public Button moreInfo;

    private bool equippable = false;
    private bool stackable = true;

    private int amount = 0;

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
        Inventory inventory = UIManager.GetMenu<Inventory>();
        inventory.RemoveInventoryItem(ii.ingredient, ii.amount);
        CameraEquip.Equip(ii.ingredient.id);
    }


    private void OpenToolTip()
    {
        if(UIManager.GetMenu<Container>().IsActive)
        {
            TransferToolTip toolTip = UIManager.Open<TransferToolTip>();
            toolTip.Open(ii, true);
        }
        else
        {
            ToolTip toolTip = UIManager.Open<ToolTip>();
            toolTip.Open(ii.ingredient);
        }
        
    }


    private void Awake()
    {
        moreInfo.onClick.AddListener(OpenToolTip);
    }
}
