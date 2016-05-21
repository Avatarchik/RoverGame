using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Sol
{
    [System.Serializable]
    public class QuestObjective 
    {
        public enum Speaker { AI, Human };

        public SoundControls soundControls; 
        public Speaker speaker = Speaker.Human;
        [TextArea]
        public string displayText ="";
        public List<GameObject> waypointTargets = new List<GameObject>();
        public bl_HudInfo defaultHudInfo;

        public QuestTrigger questTrigger;

        public float delayedClose = 4f;

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

            spawnedHudInfos.Clear();
        }


        public void Initialize()
        {
            UIManager.GetMenu<ObjectiveTracker>().Open(displayText, speaker == Speaker.Human, true, delayedClose);
            questTrigger.Initialize();

            foreach(GameObject target in waypointTargets)
            {
                bl_HudInfo info = GetHudInfo(target.transform);
                spawnedHudInfos.Add(info);
                CachedHudManager.CreateHud(info);
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

            newHudInfo.m_Target = target;
            newHudInfo.m_Color = defaultHudInfo.m_Color;
            newHudInfo.m_Icon = defaultHudInfo.m_Icon;
            newHudInfo.m_MaxSize = defaultHudInfo.m_MaxSize;
            newHudInfo.m_Text = defaultHudInfo.m_Text;
            newHudInfo.m_TypeHud = defaultHudInfo.m_TypeHud;
            newHudInfo.Offset = defaultHudInfo.Offset;
            newHudInfo.ShowDistance = defaultHudInfo.ShowDistance;
            newHudInfo.ShowDynamically = defaultHudInfo.ShowDynamically;
            newHudInfo.tip = defaultHudInfo.tip;
            newHudInfo.Arrow = defaultHudInfo.Arrow;
            newHudInfo.Hide = defaultHudInfo.Hide;
            newHudInfo.HideOnCloseDistance = defaultHudInfo.HideOnCloseDistance;
            newHudInfo.HideOnLargeDistance = defaultHudInfo.HideOnLargeDistance;

            return newHudInfo;
        }
    }
}