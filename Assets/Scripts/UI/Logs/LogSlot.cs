using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LogSlot : MonoBehaviour
{

    public delegate void SelectLogSlot(LogSlot logSlot);
    public static event SelectLogSlot OnSelectLogSlot;

    public Button selectButton;

    public Image mainImage;
    public Text titleText;

    public Color activeColor;
    public Color inactiveColor;

    public Log log;

    private bool isSelected = false;

    public bool IsSelected
    {
        get { return isSelected; }
        set
        {
            isSelected = value;
            if (isSelected)
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
        OnSelectLogSlot(this);
    }

    private void Awake()
    {
        selectButton.onClick.AddListener(SelectMe);
    }
}
