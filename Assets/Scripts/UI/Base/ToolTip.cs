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

        /// <summary>
        /// Open tooltip and initialize to ingredient values
        /// </summary>
        /// <param name="ingredient"></param>
        public virtual void Open(Ingredient ingredient)
        {
            titleText.text = ingredient.displayName;
            descriptionText.text = ingredient.description;

            gameObject.GetComponent<RectTransform>().position = Input.mousePosition;
            Open();
        }

        /// <summary>
        /// initialize to ingredient details
        /// </summary>
        /// <param name="ingredient"></param>
        public void SetContent(Ingredient ingredient)
        {
            titleText.text = ingredient.displayName;
            descriptionText.text = ingredient.description;

            gameObject.GetComponent<RectTransform>().position = new Vector3 (Input.mousePosition.x - 1024f, Input.mousePosition.y - 768f, 0f);
        }


        protected virtual void Awake()
        {
            closeButton.onClick.AddListener(Close);
        }
    }
}