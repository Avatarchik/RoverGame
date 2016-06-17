using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Colorful;


namespace Sol
{
    public class QuestManager : MonoBehaviour
    {
        public AudioMixerGroup radioAmg;
        public AudioMixerGroup aiAmg;

        public bool testMode = true;
        public List<Quest> quests = new List<Quest>();

        public float characterDelay = 0.1f;

        public Glitch[] glitchEffects;
        public bl_HudInfo defaultHudInfo;

        protected int currentQuest = 0;

        [HideInInspector]
        public bool canProceed = false;

        private bool endQuest = false;
        private int targetQuest = 0;

        public Quest CurrentQuest
        {
            get { return quests[currentQuest]; }
        }


        public virtual void SetProceed(bool b, int i)
        {
            canProceed = true;
            endQuest = b;
            targetQuest = i;
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


        public void CleanupAndRestart()
        {
            StartCoroutine(Restart());
        }


        protected IEnumerator Restart()
        {
            yield return new WaitForSeconds(1.5f);
            UIManager.GetMenu<FadeMenu>().Fade(0.2f, Color.clear, Color.black, true);
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene(3);
            Destroy(GameManager.Instance.gameObject);
        }


        protected IEnumerator DisplayDialogueCoroutine(List<DisplayDialogue> displayTexts, QuestObjective qo, bool isHuman = true)
        {
            for(int i = 0; i < displayTexts.Count; i++)
            {
                qo.questTrigger.Initialize();
                DisplayDialogue dd = displayTexts[i];

                float delay = dd.displayText.Length * characterDelay;
                
                float desiredTime = delay;

                if (dd.clip != null)
                {
                    desiredTime = dd.clip.length;
                    delay = dd.clip.length;
                    SoundSource ss = GameManager.Get<SoundManager>().Play(dd.clip);
                    if(isHuman && radioAmg != null) ss.cachedAudioSource.outputAudioMixerGroup = radioAmg;
                    if (!isHuman && aiAmg != null) ss.cachedAudioSource.outputAudioMixerGroup = aiAmg;
                }
                UIManager.GetMenu<ObjectiveTracker>().Open(dd.displayText, isHuman, true, delay);
                desiredTime += 0.75f;

                switch(dd.effect)
                {
                    case Sol.DisplayDialogue.DisplayEffect.Glitch:
                        StartCoroutine(GlitchOut(dd.duration));
                        break;

                    case Sol.DisplayDialogue.DisplayEffect.FadeIn:
                        UIManager.GetMenu<FadeMenu>().Fade(dd.duration, Color.black, Color.clear);
                        break;

                    case Sol.DisplayDialogue.DisplayEffect.FadeOut:
                        UIManager.GetMenu<FadeMenu>().Fade(dd.duration, Color.clear, Color.black, true);
                        break;

                    case Sol.DisplayDialogue.DisplayEffect.None:
                    default:
                        break;
                }

                float elapsedTime = 0f;

                while(elapsedTime < desiredTime)
                {
                    elapsedTime += Time.deltaTime;
                    //if (GameManager.Get<QuestManager>().testMode && Input.GetKeyDown(KeyCode.Return)) break;
                    yield return null;
                }
            }

            while(!canProceed)
            {
                yield return null;
            }

            CurrentQuest.CompleteObjective(endQuest, targetQuest);
            canProceed = false;
        }


        protected IEnumerator GlitchOut(float waitTime = 1f)
        {
            foreach(Glitch glitchEffect in glitchEffects)
            {
                glitchEffect.enabled = true;
            }

            yield return new WaitForSeconds(waitTime);

            foreach (Glitch glitchEffect in glitchEffects)
            {
                glitchEffect.enabled = false;
            }
        }


        protected void Awake()
        {
            BeginQuest();
            StartCoroutine(GlitchOut(3));
        }
    }
}

