using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    [System.Serializable]
    public class Quest
    {
        public string name = "quest";
        public int nextQuest = 0;
        public bool autoProceed = true;

        public List<QuestObjective> objectives = new List<QuestObjective>();
        public List<QuestTrigger> triggers = new List<QuestTrigger>();

        protected int currentObjective = 0;

        public QuestObjective CurrentObjective
        {
            get
            {
                return objectives[currentObjective];
            }
        }


        public void CleanupQuest()
        {
            foreach(QuestTrigger trigger in triggers)
            {
                trigger.initialized = false;
                trigger.StopAllCoroutines();
                trigger.gameObject.SetActive(false);
            }
        }


        public void CompleteObjective(bool endQuest, int questChoice = 0)
        {
            //cleanup the old
            if(currentObjective < objectives.Count) CurrentObjective.Cleanup();

            currentObjective++;

            //initialize the new(if there is one)
            if(currentObjective < objectives.Count && !endQuest)
            {
                CurrentObjective.Initialize();
            }
            else
            {
                currentObjective = 0;
                CleanupQuest();
                GameManager.Get<QuestManager>().CompleteQuest(questChoice);
            }
        }


        public void Initialize()
        {
            if(objectives.Count > 0) CurrentObjective.Initialize();

            QuestTrigger.onCompleteObjective += CompleteObjective;
        }
    }
}

