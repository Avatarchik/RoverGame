using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransferModal : Menu
{
    public Slider slider;
    public int transferMaximum;
    public Container container;
    public Button cancelButton;
    public Button confirmTransferButton;
    public Text selectedAmountDisplay;

    public bool toInventory;

    private int selectedAmount;


    public override void Open()
    {
        base.Open();
    }


    public void Open(int i, bool transferToInventory)
    {
        foreach(ContainerSlot cs in container.containerSlots)
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
        foreach (ContainerSlot cs in container.containerSlots)
        {
            //cs.transferButton.interactable = true;
        }
        container.root.SetActive(true);
        container.playerInventory.root.SetActive(true);
        base.Close();
    }


    public void Transfer()
    {
        if(toInventory)
        {
            container.playerInventory.AddInventoryItem(container.selectedIngredient, Mathf.RoundToInt(slider.value));
            container.RemoveInventoryItem(container.selectedIngredient, Mathf.RoundToInt(slider.value));
            Close();
        }
        else
        {
            container.playerInventory.RemoveInventoryItem(container.selectedIngredient, Mathf.RoundToInt(slider.value));
            container.AddInventoryItem(container.selectedIngredient, Mathf.RoundToInt(slider.value));
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
