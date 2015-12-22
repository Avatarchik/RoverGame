using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransferModal : Menu
{
    public Slider slider;
    public int transferMaximum;


    public override void Open()
    {
        base.Open();
    }


    public void Open(int i)
    {
        slider.maxValue = i;
        Open();
    }
}
