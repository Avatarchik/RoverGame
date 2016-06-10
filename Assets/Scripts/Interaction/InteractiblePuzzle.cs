using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class InteractiblePuzzle : InteractibleObject
    {

		public delegate void PuzzleComplete ();
		public static event PuzzleComplete onPuzzleComplete;

        public AudioClip puzzleCompleteEffect;
        public Object missionObject;
        public Object levelObject;
        public UIEvents uiEvents;

        public PuzzleManager puzzleManager;
		public GameObject myPuzzleCanvas;
		public Collider scaleTriggerColl;
		private bool firstInteraction = true;
		public bool puzzleScaled = false;

        public string message;

        public List<GameObject> objectsToActivate = new List<GameObject>();
        public List<GameObject> objectsToDeactivate = new List<GameObject>();
		public List<Collider> collidersToDeactivate = new List<Collider>();
        public List<InteractibleObject> objectsToTrigger = new List<InteractibleObject>();

        public bool questTrigger = false;

		protected bool complete = false;

        public virtual bool Complete
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

					foreach (Collider co in collidersToDeactivate)
					{
						co.enabled = false;
					}

                    foreach(InteractibleObject io in objectsToTrigger)
                    {
                        io.Interact();
                    }
					interactible = false;
                    if(puzzleCompleteEffect != null) GameManager.Get<SoundManager>().Play(puzzleCompleteEffect);
					myPuzzleCanvas.GetComponent<PuzzleAnimHandler> ().BlinkLight ();
					onPuzzleComplete ();
                }
            }
        }

        public UIEvents UiEvents
        {
            get { return (uiEvents != null) ? uiEvents : uiEvents = GameObject.FindObjectOfType<UIEvents>(); }
        }

        public override void Interact()
        {
			if(interactible && puzzleScaled)
            {
				base.Interact ();
				UiEvents.MissionButtonEvent (missionObject);
				UiEvents.LevelButtonEvent (levelObject);

				PuzzleMenu pu = UIManager.GetMenu<PuzzleMenu> ();
				pu.Open (this);
				puzzleManager.InitializePuzzle (myPuzzleCanvas);

				Camera.main.GetComponentInParent<RotateToObject> ().RotateTo (myPuzzleCanvas.transform);
				scaleTriggerColl.enabled = false;

				interactible = false;
				UIManager.Close<MessageMenu> ();

				if (firstInteraction) {
					myPuzzleCanvas.GetComponent<PuzzleAnimHandler> ().ActivateLight ();
					firstInteraction = false;
				}
            }
        }
    }
}