using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class QuestManager : MonoBehaviour
    {
        
        public List<Quest> quests = new List<Quest>();

        protected int currentQuest = 0;

        public Quest CurrentQuest
        {
            get { return quests[currentQuest]; }
        }


        public virtual void CompleteQuest()
        {
            currentQuest++;
            if(currentQuest < quests.Count)
            {
                BeginQuest();
            }
        }


        public virtual void CompleteQuest(int i)
        {
            currentQuest = i;
            if (currentQuest < quests.Count)
            {
                BeginQuest();
            }
        }


        public virtual void BeginQuest()
        {
            CurrentQuest.Initialize();
        }


        private void Awake()
        {
            BeginQuest();
        }
    }
}

