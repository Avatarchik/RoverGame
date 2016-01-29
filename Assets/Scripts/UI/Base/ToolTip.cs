using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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


    private void Awake()
    {
        closeButton.onClick.AddListener(Close);
    }
}
