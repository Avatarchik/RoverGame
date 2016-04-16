using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
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


        public void Open(string message, int priority = 0, float delayedClose = 0f)
        {
            if (!isActive && priority >= CurrentMessagePriority)
            {
                currentMessagePriority = priority;
                messageText.text = message;
                Open();

                if (delayedClose != 0f)
                {
                    StopAllCoroutines();
                    StartCoroutine(DelayedClose(delayedClose));
                }
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
            if (isActive)
            {
                currentMessagePriority = -1;
                base.Close();
            }
        }


        private IEnumerator DelayedClose(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            Close();
        }
    }
}