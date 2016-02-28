using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class InventoryInfoPanel : MonoBehaviour
    {
        public Text title;
        public Image image;
        public Text description;
        public Text amountText;

        public Button dropButton;
        public Button useButton;


        public void Initialize(InventoryIngredient ii)
        {
            title.text = ii.ingredient.displayName;
            image.sprite = ii.ingredient.image;
            description.text = ii.ingredient.description;
            amountText.text = ii.amount.ToString();
        }


        public void Initialize(Ingredient i, int amount)
        {
            title.text = i.displayName;
            image.sprite = i.image;
            description.text = i.description;
            amountText.text = amount.ToString();
        }


        private void DropItem()
        {
            //TODO implement logic for dropping items
        }


        private void UseItem()
        {

        }


        private void TransferItem()
        {

        }
    }
}

