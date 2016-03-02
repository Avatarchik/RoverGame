using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LogMenu : Menu
{
    public LogInfoPanel logInfoPanel;
    public Transform logSlotContainer;
    public LogSlot logSlotPrefab;

    public List<Log> logs = new List<Log>();
    private List<LogSlot> logSlots = new List<LogSlot>();

    private bool canClose = true;

    public override void Open()
    {
        if (!isActive)
        {
            InitializeSlots(logs);
            base.Open();
        }
    }


    public override void Close()
    {
        if (isActive && canClose)
        {
            base.Close();
        }
    }


    public void AddLog(Log log)
    {
        if (!logs.Contains(log)) logs.Insert(0, log); 
    }


    public void SelectLogSlot(LogSlot logSlot)
    {
        foreach (LogSlot ls in logSlots)
        {
            ls.IsSelected = false;
        }

        logSlot.IsSelected = true;
        logInfoPanel.SelectedLog = logSlot.log;
    }


    private void InitializeSlots(List<Log> logList)
    {
        //TODO optomize this to reuse slots!
        foreach (LogSlot cs in logSlots)
        {
            Destroy(cs.gameObject);
        }
        logSlots.Clear();

        foreach (Log log in logs)
        {
            LogSlot newLogSlot = Instantiate(logSlotPrefab) as LogSlot;
            newLogSlot.transform.SetParent(logSlotContainer);
            newLogSlot.titleText.text = log.header;
            newLogSlot.log = log;

            logSlots.Add(newLogSlot);
        }

        if (logSlots.Count > 0) SelectLogSlot(logSlots[0]);
    }


    private void Awake()
    {
        LogSlot.OnSelectLogSlot += SelectLogSlot;
    }
}
