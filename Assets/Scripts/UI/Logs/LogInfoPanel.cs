using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LogInfoPanel : MonoBehaviour
{
    public Text titleText;
    public Text descriptionText;

    private Log selectedLog;


    public Log SelectedLog
    {
        get { return selectedLog; }
        set
        {
            selectedLog = value;
            titleText.text = selectedLog.header;
            descriptionText.text = selectedLog.content;
        }
    }
}
