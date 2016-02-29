using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Sol
{
    public class ContainerSlot : InventorySlot
    {
        public Button transferbutton;
        private void OpenTransferToolTip()
        {
            Inventory inventory = UIManager.GetMenu<Inventory>();
            Container container = UIManager.GetMenu<Container>();

            if(amount > 5)
            {
                //need to pull up a modal
            }
            else
            {
                //throw one over
                inventory.AddInventoryItem(SlotIngredient, 1);
                container.RemoveInventoryItem(SlotIngredient, 1);
            }
        }


        private void Awake()
        {
            transferbutton.onClick.AddListener(OpenTransferToolTip);
        }
    }

}
