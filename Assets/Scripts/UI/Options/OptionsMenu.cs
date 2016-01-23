using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsMenu : Menu
{
    public Button backButton;


    public override void Open()
    {
        base.Open();
    }


    public override void Close()
    {
        base.Close();
    }


    private void Awake()
    {
        backButton.onClick.AddListener(Close);
    }
}
