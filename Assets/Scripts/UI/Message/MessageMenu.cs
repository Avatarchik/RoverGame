using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageMenu : Menu
{
    public Text messageText;

    private int currentMessagePriority = -1;

    public int CurrentMessagePriority
    {
        get { return currentMessagePriority; }
    }

    public override void Open()
    {
        base.Open();
    }


    public void Open(string message, int priority = 0)
    {
        if (!isActive && priority >= CurrentMessagePriority)
        {
            currentMessagePriority = priority;
            messageText.text = message;
            Open();
        }
    }


    public void SetText(string message, int priority = 0)
    {
        if (priority >= CurrentMessagePriority)
        {
            currentMessagePriority = priority;
            messageText.text = message;
        }
    }


    public override void Close()
    {
        if(isActive)
        {
            currentMessagePriority = -1;
            base.Close();
        }
    }
}
