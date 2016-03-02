using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class InventorySlot : MonoBehaviour
    {
        
        public InventoryInfoPanel infoPanel;

        public Image image;
        public Text displayText;
        public Text amountText;
        public Toggle moreInfo;

        private bool equippable = false;
        private bool stackable = true;

        protected string amountString = "({0})";
        protected int amount = 0;
        protected Ingredient slotIngredient;


        public int Amount
        {
            get { return amount;  }
            set
            {                
                amount = value;
                if (amountText != null && amount > 1)
                {
                    amountText.text = string.Format(amountString, amount);
                }
                else
                {
                    amountText.text = "";
                }
            }
        }


        public Ingredient SlotIngredient
        {
            get { return slotIngredient; }
            set
            {
                if(value != null)
                {
                    slotIngredient = value;
                    displayText.text = slotIngredient.displayName;
                }
            }
        }


        private void Initialize(bool b)
        {
            if(UIManager.GetMenu<Inventory>().ContainerExchange)
            {
                if(b)
                {
                    Container container = UIManager.GetMenu<Container>();
                    Inventory inventory = UIManager.GetMenu<Inventory>();

                    //need to handle transfer
                    if (amount > 5)
                    {
                        
                    }
                    else
                    {
                        //just throw one over
                        container.AddInventoryItem(SlotIngredient, 1);
                        inventory.RemoveInventoryItem(SlotIngredient, 1);
                    }
                }
            }
            else
            {
                //just need to initialize the panel
                //if (b) infoPanel.Initialize(SlotIngredient, Amount);
            }
        }


        private void Awake()
        {
            moreInfo.onValueChanged.AddListener(Initialize);
        }
    }
}
