using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class InteractiblePuzzle : InteractibleObject
    {
        public Object missionObject;
        public Object levelObject;
        public UIEvents uiEvents;

        public PuzzleManager puzzleManager;

        public UIEvents UiEvents
        {
            get { return (uiEvents != null) ? uiEvents : uiEvents = GameObject.FindObjectOfType<UIEvents>(); }
        }

        public override void Interact()
        {
            base.Interact();
            uiEvents.MissionButtonEvent(missionObject);
            uiEvents.LevelButtonEvent(levelObject);

            UIManager.Open<PuzzleMenu>();
            puzzleManager.ForceLoad();
        }
    }
}