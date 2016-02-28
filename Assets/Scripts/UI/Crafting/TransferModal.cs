using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Sol
{
    public class TransferModal : Menu
    {
        public Slider slider;
        public Button cancelButton;
        public Button confirmTransferButton;
        public Text selectedAmountDisplay;

        private int selectedAmount;
        private int transferMax = 0;
        private Ingredient ingredient = null;
        private bool toInventory = false;


        public override void Open()
        {
            base.Open();
        }


        public void Open(int i, InventoryIngredient ii, bool transferToInventory)
        {
            toInventory = transferToInventory;
            ingredient = ii.ingredient;

            slider.maxValue = i;
            slider.value = 0;
            Open();
        }


        public override void Close()
        {
            base.Close();
        }


        public void Transfer()
        {
            Inventory playerInventory = UIManager.GetMenu<Inventory>();
            Container container = UIManager.GetMenu<Container>();

            if (toInventory)
            {
                playerInventory.AddInventoryItem(ingredient, Mathf.RoundToInt(slider.value));
                container.RemoveInventoryItem(ingredient, Mathf.RoundToInt(slider.value));
                Close();
            }
            else
            {
                playerInventory.RemoveInventoryItem(ingredient, Mathf.RoundToInt(slider.value));
                container.AddInventoryItem(ingredient, Mathf.RoundToInt(slider.value));
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
}