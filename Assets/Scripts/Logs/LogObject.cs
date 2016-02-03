using UnityEngine;
using System.Collections;

public class LogObject : InteractibleObject
{
    public Log log;

    public override void Interact()
    {
        if (Interactible)
        {
            LogMenu logMenu = UIManager.GetMenu<LogMenu>();
            logMenu.AddLog(log);
            logMenu.Open();
        }
    }
}
