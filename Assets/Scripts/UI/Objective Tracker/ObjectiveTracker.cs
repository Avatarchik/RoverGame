using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class ObjectiveTracker : Menu
    {
        public ObjectiveDisplay objectivePrefab;
        public Transform objectiveContainer;

        public Text objectiveTextAdmin;
        public Text objectiveTextAI;

        public List<ObjectiveDisplay> displayedObjectives = new List<ObjectiveDisplay>();


        public override void Open()
        {
            base.Open();
        }


        public void Open(string objective, bool admin = true, bool delayedClose = false, float delayedCloseTime = 4f)
        {
            if (admin)
            {
                objectiveTextAdmin.text = objective;
                objectiveTextAI.text = "";
            }
            else
            {
                objectiveTextAdmin.text = "";
                objectiveTextAI.text = objective;
            }

            Open();
            if (delayedClose) StartCoroutine(DelayedClose(delayedCloseTime));
        }


        private IEnumerator DelayedClose(float delayedCloseTime)
        {
            yield return new WaitForSeconds(delayedCloseTime);

            Close();
        }
    }

}