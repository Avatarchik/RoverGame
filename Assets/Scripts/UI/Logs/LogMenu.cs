using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class LogMenu : Menu
    {
        private const string NEW_RADIO_LOG_FORMAT = "\n \n <color=#{0}>{1}</color>";

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


        public void AddRadioLog(string radioLog, string color)
        {
            Log masterLog = logs[0];
            masterLog.content += string.Format(NEW_RADIO_LOG_FORMAT, color, radioLog);

            if (logInfoPanel.SelectedLog == masterLog) logInfoPanel.SelectedLog = masterLog;
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

            logs[0].content = "";
        }
    }

}
