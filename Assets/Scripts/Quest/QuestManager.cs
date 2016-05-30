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


        public virtual void CompleteQuest(int i)
        {
            CurrentQuest.iscurrentQuest = false;
            if (!CurrentQuest.autoProceed) return;
            currentQuest = i;
            if (currentQuest < quests.Count)
            {
                BeginQuest();
            }
        }


        public virtual void BeginQuest()
        {
            Debug.Log("Beginning Quest : " + currentQuest +" | "+CurrentQuest.name);
            CurrentQuest.Initialize();
        }


        public virtual void DisplayDialogue(List<string> displayTexts, QuestObjective qo, bool isHuman = true)
        {
            StartCoroutine(DisplayDialogueCoroutine(displayTexts, qo, isHuman));
        }


        protected static IEnumerator DisplayDialogueCoroutine(List<string> displayTexts, QuestObjective qo, bool isHuman = true)
        {
            for(int i = 0; i < displayTexts.Count; i++)
            {
                if (i == displayTexts.Count - 1) qo.questTrigger.Initialize();

                float delay = displayTexts[i].Length * 0.085f;
                UIManager.GetMenu<ObjectiveTracker>().Open(displayTexts[i], isHuman, true, delay);
                yield return new WaitForSeconds(delay + 0.5f);
            }
        }


        protected void Awake()
        {
            BeginQuest();
        }
    }
}

