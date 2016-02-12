using UnityEngine;
using System.Collections;

public class LogObject : InteractibleObject
{
    public Log log;

    public override void Interact()
    {
        Debug.Log("1");
        if (Interactible)
        {
            Debug.Log("2");
            LogMenu logMenu = UIManager.GetMenu<LogMenu>();
            
            logMenu.AddLog(log);
            logMenu.Open();
        }
    }
}
