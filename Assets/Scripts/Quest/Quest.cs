using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    [System.Serializable]
    public class Quest
    {
        public string name = "quest";

        public List<QuestObjective> objectives = new List<QuestObjective>();

        protected int currentObjective = 0;

        public QuestObjective CurrentObjective
        {
            get
            {
                return objectives[currentObjective];
            }
        }


        public void CompleteObjective()
        {
            //cleanup the old
            CurrentObjective.Cleanup();

            currentObjective++;

            //initialize the new(if there is one)
            if(currentObjective < objectives.Count)
            {
                CurrentObjective.Initialize();
            }
            else
            {
                GameManager.Get<QuestManager>().CompleteQuest();
            }
        }


        public void Initialize()
        {
            if(objectives.Count > 0) CurrentObjective.Initialize();

            QuestTrigger.onCompleteObjective += CompleteObjective;
        }
    }
}

