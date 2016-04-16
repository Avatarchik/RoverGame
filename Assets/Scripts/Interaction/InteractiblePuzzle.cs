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

        public string message;

        public List<GameObject> objectsToActivate = new List<GameObject>();
        public List<GameObject> objectsToDeactivate = new List<GameObject>();
        public List<InteractibleObject> objectsToTrigger = new List<InteractibleObject>();

        public bool questTrigger = false;

        private bool complete = false;

        public bool Complete
        {
            get { return complete; }
            set
            {
                complete = value;
                if(complete)
                {
                    if (questTrigger) GameObject.FindObjectOfType<Intro>().Next();

                    foreach(GameObject go in objectsToActivate)
                    {
                        go.SetActive(true);
                    }

                    foreach (GameObject go in objectsToDeactivate)
                    {
                        go.SetActive(false);
                    }

                    foreach(InteractibleObject io in objectsToTrigger)
                    {
                        io.Interact();
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
                UiEvents.MissionButtonEvent(missionObject);
                UiEvents.LevelButtonEvent(levelObject);

                PuzzleMenu pu = UIManager.GetMenu<PuzzleMenu>();
                pu.Open(this);
                puzzleManager.InitializePuzzle();

                interactible = false;

                UIManager.Close<MessageMenu>();
            }
        }
    }
}