using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class InteractiblePuzzle : InteractibleObject
    {
        public Object missionObject;
        public Object levelObject;
        public UIEvents uiEvents;

        public PuzzleManager puzzleManager;
        public List<GameObject> objectsToActivate = new List<GameObject>();
        public List<GameObject> objectsToDeactivate = new List<GameObject>();

        private bool complete = false;

        public bool Complete
        {
            get { return complete; }
            set
            {
                complete = value;
                if(complete)
                {
                    foreach(GameObject go in objectsToActivate)
                    {
                        go.SetActive(true);
                    }

                    foreach (GameObject go in objectsToDeactivate)
                    {
                        go.SetActive(false);
                    }
                }
            }
        }

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