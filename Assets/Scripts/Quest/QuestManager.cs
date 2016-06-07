using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class QuestManager : MonoBehaviour
    {
        public bool testMode = true;
        public List<Quest> quests = new List<Quest>();

        public float characterDelay = 0.1f;

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


        public virtual void DisplayPermanentObjective(string objective)
        {
            UIManager.GetMenu<ObjectiveTracker>().ShowPermanentObjective(objective);
        }


        protected IEnumerator DisplayDialogueCoroutine(List<string> displayTexts, QuestObjective qo, bool isHuman = true)
        {
            for(int i = 0; i < displayTexts.Count; i++)
            {
                if (i == displayTexts.Count - 1) qo.questTrigger.Initialize();

                float delay = displayTexts[i].Length * characterDelay;
                UIManager.GetMenu<ObjectiveTracker>().Open(displayTexts[i], isHuman, true, delay);

                

                float elapsedTime = 0f;
                float desiredTime = delay + 0.75f;

                while(elapsedTime < desiredTime)
                {
                    elapsedTime += Time.deltaTime;
                    if (GameManager.Get<QuestManager>().testMode && Input.GetKeyDown(KeyCode.Return)) break;
                    yield return null;
                }
            }
        }


        protected void Awake()
        {
            BeginQuest();
        }
    }
}

