using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace Sol
{
    public class ToolTip : Menu
    {
        public Text titleText;
        public Text descriptionText;


        public Button closeButton;


        public override void Close()
        {
            base.Close();
        }


        public override void Open()
        {
            base.Open();
        }


        public virtual void Open(Ingredient ingredient)
        {
            titleText.text = ingredient.displayName;
            descriptionText.text = ingredient.description;

            gameObject.GetComponent<RectTransform>().position = Input.mousePosition;
            Open();
        }


        public void SetContent(Ingredient ingredient)
        {
            titleText.text = ingredient.displayName;
            descriptionText.text = ingredient.description;

            gameObject.GetComponent<RectTransform>().position = Input.mousePosition;
        }


        protected virtual void Awake()
        {
            closeButton.onClick.AddListener(Close);
        }
    }

}