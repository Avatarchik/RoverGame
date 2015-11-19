using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CraftingSlot : MonoBehaviour
{
    public delegate void SelectCraftingSlot(CraftingSlot craftingSlot);
    public static event SelectCraftingSlot OnSelectCraftingSlot;

    public Button selectButton;

    public Image mainImage;
    public Image iconImage;
    public Text titleText;

    public Color activeColor;
    public Color inactiveColor;

    public Item Item;

    private bool isSelected = false;

    public bool IsSelected
    {
        get { return isSelected; }
        set
        {
            isSelected = value;
            if(isSelected)
            {
                mainImage.color = activeColor;
            }
            else
            {
                mainImage.color = inactiveColor;
            }
        }
    }

    private void SelectMe()
    {
        OnSelectCraftingSlot(this);
    }

    private void Awake()
    {
        selectButton.onClick.AddListener(SelectMe);
    }
}
