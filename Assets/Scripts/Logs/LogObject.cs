using UnityEngine;
using System.Collections;

namespace Sol
{
    public class LogObject : InteractibleObject
    {
        public Log log;

        public override void Interact()
        {
            if (Interactible)
            {
                LogMenu logMenu = UIManager.GetMenu<LogMenu>();
                InGameMainMenu igmm = UIManager.GetMenu<InGameMainMenu>();

                logMenu.AddLog(log);
                igmm.OpenLogs();

				Destroy (gameObject);
            }
        }
    }
}