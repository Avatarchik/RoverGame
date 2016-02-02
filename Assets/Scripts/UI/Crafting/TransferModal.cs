using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransferModal : Menu
{
    public Slider slider;
    public int transferMaximum;
    public Button cancelButton;
    public Button confirmTransferButton;
    public Text selectedAmountDisplay;

    public bool toInventory;

    private int selectedAmount;
    private Container containerInstance;


    public Container ContainerInstance
    {
        get { return (containerInstance != null) ? containerInstance : containerInstance = UIManager.GetMenu<Container>(); }
    }


    public override void Open()
    {
        base.Open();
    }


    public void Open(int i, bool transferToInventory)
    {
        foreach(ContainerSlot cs in ContainerInstance.containerSlots)
        {
            //cs.transferButton.interactable = false;
        }

        toInventory = transferToInventory;

        slider.maxValue = i;
        slider.value = 0;
        Open();
    }


    public override void Close()
    {
        foreach (ContainerSlot cs in ContainerInstance.containerSlots)
        {
            //cs.transferButton.interactable = true;
        }
        ContainerInstance.root.SetActive(true);
        UIManager.GetMenu<Inventory>().root.SetActive(true);
        base.Close();
    }


    public void Transfer()
    {
        if(toInventory)
        {
            UIManager.GetMenu<Inventory>().AddInventoryItem(ContainerInstance.selectedIngredient, Mathf.RoundToInt(slider.value));
            ContainerInstance.RemoveInventoryItem(ContainerInstance.selectedIngredient, Mathf.RoundToInt(slider.value));
            Close();
        }
        else
        {
            UIManager.GetMenu<Inventory>().RemoveInventoryItem(ContainerInstance.selectedIngredient, Mathf.RoundToInt(slider.value));
            ContainerInstance.AddInventoryItem(ContainerInstance.selectedIngredient, Mathf.RoundToInt(slider.value));
            Close();
        }
        
    }


    public void UpdateAmountText(float i)
    {
        selectedAmountDisplay.text = Mathf.RoundToInt(i) + "";
    }


    private void Start()
    {
        slider.onValueChanged.AddListener(UpdateAmountText);
        confirmTransferButton.onClick.AddListener(Transfer);
        cancelButton.onClick.AddListener(Close);
    }
}
