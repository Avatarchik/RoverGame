using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CraftingSlot : MonoBehaviour
{
    public delegate void SelectCraftingSlot(CraftingSlot craftingSlot);
    public static event SelectCraftingSlot OnSelectCraftingSlot;

    public Toggle selectToggle;

    public Image mainImage;
    public Image iconImage;
    public Text titleText;

    public Recipe recipe;
    public Ingredient ingredient;

    private bool isSelected = false;

    public bool IsSelected
    {
        get { return isSelected; }
        set { isSelected = value; }
    }

    private void SelectMe(bool b)
    {
        if(b) OnSelectCraftingSlot(this);
        IsSelected = b;
    }

    private void Awake()
    {
        selectToggle.onValueChanged.AddListener(SelectMe);
    }
}
