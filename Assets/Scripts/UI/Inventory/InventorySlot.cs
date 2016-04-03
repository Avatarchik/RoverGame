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
                    if(slotIngredient as EquipableItem)
                    {
                        EquipableItem ei = (EquipableItem)slotIngredient;
                        image.color = ei.equipmentColor;
                    }
                }
            }
        }


        public virtual void OpenHoverTooltip()
        {
            HoverTip hoverTooltip = UIManager.GetMenu<HoverTip>();
            hoverTooltip.Open(slotIngredient.displayName, slotIngredient.description, Input.mousePosition);
        }


        public virtual void CloseHoverTooltip()
        {
            UIManager.Close<HoverTip>();
        }


        private void Initialize(bool b)
        {
            Inventory inventory = UIManager.GetMenu<Inventory>();

            if (UIManager.GetMenu<Inventory>().ContainerExchange)
            {
                if(b)
                {
                    Container container = UIManager.GetMenu<Container>();
                    

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
                //not exchanging!!
                if(slotIngredient as EquipableItem)
                {
                    EquipableItem ei = (EquipableItem)slotIngredient;
                    inventory.RemoveInventoryItem(slotIngredient, 1);

                    inventory.equipmentPanel.EquipItem(ei);

                    inventory.InitializeInventorySlots();

                    inventory.equipmentPanel.Initialize();
                }
                else
                {
                    //????
                }
            }

            CloseHoverTooltip();
        }


        private void Awake()
        {
            moreInfo.onValueChanged.AddListener(Initialize);
        }
    }
}
