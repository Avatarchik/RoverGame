using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageMenu : Menu
{
    public Text messageText;

    public override void Open()
    {
        base.Open();
    }


    public void Open(string message)
    {
        messageText.text = message;
        Open();
    }


    public override void Close()
    {
        base.Close();
    }
}
