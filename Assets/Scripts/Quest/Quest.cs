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

        public List<QuestObjective> objectives = new List<QuestObjective>();

        protected int currentObjective = 0;

        public QuestObjective CurrentObjective
        {
            get
            {
                return objectives[currentObjective];
            }
        }


        public void CompleteObjective(bool hasTargetQuest = false, int questChoice = 0)
        {
            //cleanup the old
            if(currentObjective < objectives.Count) CurrentObjective.Cleanup();

            currentObjective++;

            //initialize the new(if there is one)
            if(currentObjective < objectives.Count)
            {
                CurrentObjective.Initialize();
            }
            else
            {
                if(hasTargetQuest)
                {
                    currentObjective = 0;
                    GameManager.Get<QuestManager>().CompleteQuest(questChoice);
                }
                else
                {
                    currentObjective = 0;
                    GameManager.Get<QuestManager>().CompleteQuest();
                }
            }
        }


        public void Initialize()
        {
            if(objectives.Count > 0) CurrentObjective.Initialize();

            QuestTrigger.onCompleteObjective += CompleteObjective;
        }
    }
}

