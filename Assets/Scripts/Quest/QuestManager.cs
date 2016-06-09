using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Colorful;


namespace Sol
{
    public class QuestManager : MonoBehaviour
    {
        public bool testMode = true;
        public List<Quest> quests = new List<Quest>();

        public float characterDelay = 0.1f;

        public Glitch glitchEffect;
        public bl_HudInfo defaultHudInfo;

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


        public virtual void DisplayDialogue(List<DisplayDialogue> displayTexts, QuestObjective qo, bool isHuman = true)
        {
            StartCoroutine(DisplayDialogueCoroutine(displayTexts, qo, isHuman));
        }


        public virtual void DisplayPermanentObjective(string objective)
        {
            UIManager.GetMenu<ObjectiveTracker>().ShowPermanentObjective(objective);
        }


        protected IEnumerator DisplayDialogueCoroutine(List<DisplayDialogue> displayTexts, QuestObjective qo, bool isHuman = true)
        {
            for(int i = 0; i < displayTexts.Count; i++)
            {
                qo.questTrigger.Initialize();
                DisplayDialogue dd = displayTexts[i];

                float delay = dd.displayText.Length * characterDelay;
                UIManager.GetMenu<ObjectiveTracker>().Open(dd.displayText, isHuman, true, delay);
                float desiredTime = delay + 0.75f;

                if (dd.clip != null)
                {
                    desiredTime = dd.clip.length;
                    GameManager.Get<SoundManager>().Play(dd.clip);
                }
                
                switch(dd.effect)
                {
                    case Sol.DisplayDialogue.DisplayEffect.Glitch:
                        StartCoroutine(GlitchOut(dd.duration));
                        break;

                    case Sol.DisplayDialogue.DisplayEffect.FadeIn:
                        UIManager.GetMenu<FadeMenu>().Fade(dd.duration, Color.black, Color.clear);
                        break;

                    case Sol.DisplayDialogue.DisplayEffect.FadeOut:
                        UIManager.GetMenu<FadeMenu>().Fade(dd.duration, Color.clear, Color.black);
                        break;

                    case Sol.DisplayDialogue.DisplayEffect.None:
                    default:
                        break;
                }

                float elapsedTime = 0f;

                while(elapsedTime < desiredTime)
                {
                    elapsedTime += Time.deltaTime;
                    if (GameManager.Get<QuestManager>().testMode && Input.GetKeyDown(KeyCode.Return)) break;
                    yield return null;
                }
            }
        }


        protected IEnumerator GlitchOut(float waitTime = 1f)
        {
            glitchEffect.enabled = true;

            yield return new WaitForSeconds(waitTime);

            glitchEffect.enabled = false;
        }


        protected void Awake()
        {
            BeginQuest();
            StartCoroutine(GlitchOut(3));
        }
    }
}

