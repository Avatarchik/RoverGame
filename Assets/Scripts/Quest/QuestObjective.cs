using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Sol
{
    [System.Serializable]
    public class DisplayDialogue
    {
        public enum DisplayEffect { None, Glitch, FadeIn, FadeOut }

        public string displayText = "";
        public DisplayEffect effect = DisplayEffect.None;
        public float duration = 0f;
        public AudioClip clip = null;
    }


    [System.Serializable]
    public class QuestObjective 
    {
        public enum Speaker { AI, Human };
        public string objectiveText = "";

        public SoundControls soundControls; 
        public Speaker speaker = Speaker.Human;

        public List<DisplayDialogue> displayTexts = new List<DisplayDialogue>();

        public List<GameObject> waypointTargets = new List<GameObject>();

        public QuestTrigger questTrigger;
        public bool isQuestChoice = false;

        protected bl_HudManager cachedHudManager;
        protected List<bl_HudInfo> spawnedHudInfos = new List<bl_HudInfo>();


        protected bl_HudManager CachedHudManager
        {
            get { return (cachedHudManager != null) ? cachedHudManager : GameManager.Get<bl_HudManager>(); }
        }


        public void Cleanup()
        {
            foreach(bl_HudInfo spawnedHudInfo in spawnedHudInfos)
            {
                CachedHudManager.RemoveHud(spawnedHudInfo);
            }

            CachedHudManager.RemoveAllHuds();
            spawnedHudInfos.Clear();
        }


        public void Initialize()
        {
            QuestManager qm = GameManager.Get<QuestManager>();
            if(objectiveText != "") qm.DisplayPermanentObjective(objectiveText);
            qm.DisplayDialogue(displayTexts, this, speaker == Speaker.Human);

            foreach(GameObject target in waypointTargets)
            {
                if(target != null)
                {
                    bl_HudInfo info = GetHudInfo(target.transform);
                    spawnedHudInfos.Add(info);
                    CachedHudManager.CreateHud(info);
                }
            }

            if (soundControls.interactEffects.Length > 0)
            {
                SoundManager sm = GameManager.Get<SoundManager>();
                switch (soundControls.soundPlayType)
                {
                    case SoundControls.PlayType.PlayAll:
                        foreach (AudioClip ac in soundControls.interactEffects) { sm.Play(ac); }
                        break;

                    case SoundControls.PlayType.PlayRandom:
                        int clipIndex = Mathf.RoundToInt(Random.Range(0, soundControls.interactEffects.Length));
                        sm.Play(soundControls.interactEffects[clipIndex]);
                        break;
                }
            }
        }


        private bl_HudInfo GetHudInfo(Transform target)
        {
            bl_HudInfo newHudInfo = new bl_HudInfo();
            QuestManager qm = GameManager.Get<QuestManager>();

            newHudInfo.m_Target = target;
            newHudInfo.m_Color = qm.defaultHudInfo.m_Color;
            newHudInfo.m_Icon = qm.defaultHudInfo.m_Icon;
            newHudInfo.m_MaxSize = qm.defaultHudInfo.m_MaxSize;
            newHudInfo.m_Text = qm.defaultHudInfo.m_Text;
            newHudInfo.m_TypeHud = qm.defaultHudInfo.m_TypeHud;
            newHudInfo.Offset = qm.defaultHudInfo.Offset;
            newHudInfo.ShowDistance = qm.defaultHudInfo.ShowDistance;
            newHudInfo.ShowDynamically = qm.defaultHudInfo.ShowDynamically;
            newHudInfo.tip = qm.defaultHudInfo.tip;
            newHudInfo.Arrow = qm.defaultHudInfo.Arrow;
            newHudInfo.Hide = qm.defaultHudInfo.Hide;
            newHudInfo.HideOnCloseDistance = qm.defaultHudInfo.HideOnCloseDistance;
            newHudInfo.HideOnLargeDistance = qm.defaultHudInfo.HideOnLargeDistance;

            return newHudInfo;
        }        
    }
}