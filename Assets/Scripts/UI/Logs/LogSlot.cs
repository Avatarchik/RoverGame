using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LogSlot : MonoBehaviour
{

    public delegate void SelectLogSlot(LogSlot logSlot);
    public static event SelectLogSlot OnSelectLogSlot;

    public Toggle selectToggle;
    public Text titleText;

    public Log log;

    private bool isSelected = false;

    public bool IsSelected
    {
        get { return isSelected; }
        set
        { isSelected = value; }
    }

    private void SelectMe(bool b)
    {
        if(b) OnSelectLogSlot(this);
		print ("ASDAS");
    }

    private void Awake()
    {
        selectToggle.onValueChanged.AddListener(SelectMe);
    }
}
