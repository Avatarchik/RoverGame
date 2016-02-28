using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class QuestObjective : MonoBehaviour
    {
        public Objective objective;
        public List<QuestTrigger> questTriggers = new List<QuestTrigger>();


        public virtual void Initialize()
        {
            ObjectiveTracker od = UIManager.GetMenu<ObjectiveTracker>();
            od.AddObjective(objective);

            foreach(QuestTrigger qt in questTriggers)
            {
                if (!qt.gameObject.activeSelf) qt.gameObject.SetActive(true);
                qt.enabled = true;
            }
        }


        public virtual void CleanUp()
        {
            foreach (QuestTrigger qt in questTriggers)
            {
                qt.enabled = false;
            }
        }
    }
}

