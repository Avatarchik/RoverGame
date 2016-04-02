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
            if(interactible)
            {
                base.Interact();
                uiEvents.MissionButtonEvent(missionObject);
                uiEvents.LevelButtonEvent(levelObject);

                PuzzleMenu pu = UIManager.GetMenu<PuzzleMenu>();
                pu.Open(this);
                puzzleManager.ForceLoad();

                interactible = false;
            }
        }
    }
}