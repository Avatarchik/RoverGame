using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class InventorySlot : MonoBehaviour
    {
        public InventoryIngredient ii;
        public InventoryInfoPanel infoPanel;

        public Image image;
        public Text amountText;
        public Toggle moreInfo;

        private bool equippable = false;
        private bool stackable = true;

        private int amount = 0;

        public int Amount
        {
            set
            {
                amount = value;
                if(amountText !=null) amountText.text = amount + "";
            }
        }


        private void Initialize(bool b)
        {
            if(b) infoPanel.Initialize(ii);
        }


        private void Awake()
        {
            if (image != null) image.sprite = ii.ingredient.image;
            moreInfo.onValueChanged.AddListener(Initialize);
        }
    }
}
